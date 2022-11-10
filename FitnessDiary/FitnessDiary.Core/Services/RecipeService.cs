using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository repo;

        public RecipeService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<string> AddAsync(CreateViewModel model)
        {
            var recipe = new Recipe()
            {
                Name = model.Name,
                ServingsSize = model.ServingsSize,
                Unit = (MeassureUnitType)model.Unit,
                Nutrition = new NutritionData(),
                UserId = model.UserId,
            };

            await repo.AddAsync<Recipe>(recipe);
            await repo.SaveChangesAsync();

            var result = await repo.GetLatest<Recipe>();
            return result.Id;
        }

        public async Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, string recepieId)
        {
            var food = await repo.All<Food>()
                .Where(f => f.Id == ingredient.FoodId)
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync();

            var recipe = await repo.All<Recipe>()
                .Where(r => r.Id == recepieId)
                .Include(r => r.Nutrition)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Food)
                .ThenInclude(f => f.Nutrition)
                .FirstOrDefaultAsync();

            recipe.Ingredients.Add(new Ingredient()
            {
                FoodId = food.Id,
                Food = food,
                Amount = ingredient.Amount,
            });

            recipe.Nutrition.Calories += (food.Nutrition.Calories * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Carbohydrates += (food.Nutrition.Carbohydrates * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Proteins += (food.Nutrition.Proteins * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Fats += (food.Nutrition.Fats * ingredient.Amount) / recipe.ServingsSize;

            await repo.SaveChangesAsync();

            var result = await GetByIdAsync(recepieId);

            return result;
        }

        public async Task<DetailsViewModel> EditAsync(EditViewModel model)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.Id == model.Id)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .ThenInclude(f => f.Nutrition)
                 .FirstOrDefaultAsync();

            recipe.Name = model.Name;
            recipe.ServingsSize = model.ServingsSize;
            recipe.Unit = (MeassureUnitType)model.Unit;
            recipe.Nutrition.Calories = 0;
            recipe.Nutrition.Carbohydrates = 0;
            recipe.Nutrition.Proteins = 0;
            recipe.Nutrition.Fats = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                var newIngredientAmount = model.Ingredients.Where(i => i.Id == ingredient.Id).FirstOrDefault().Amount;
                ingredient.Amount = newIngredientAmount;
                ingredient.Food.Nutrition.Calories = ingredient.Food.Nutrition.Calories * newIngredientAmount;
                ingredient.Food.Nutrition.Carbohydrates = ingredient.Food.Nutrition.Carbohydrates * newIngredientAmount;
                ingredient.Food.Nutrition.Proteins = ingredient.Food.Nutrition.Proteins * newIngredientAmount;
                ingredient.Food.Nutrition.Fats = ingredient.Food.Nutrition.Fats * newIngredientAmount;
            }

            CalculateNutrition(recipe);

            await repo.SaveChangesAsync();
            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                TotalCalories = recipe.Nutrition.Calories,
                Carbs = recipe.Nutrition.Carbohydrates,
                Protein = recipe.Nutrition.Proteins,
                Fats = recipe.Nutrition.Fats,
                Unit = (int)recipe.Unit,
                CaloriesPerPortion = recipe.CaloriesPerServing,
                isFinished = recipe.isFinished,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }

        private async void CalculateNutrition(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                recipe.Nutrition.Calories += ingredient.Food.Nutrition.Calories / recipe.ServingsSize;
                recipe.Nutrition.Carbohydrates += ingredient.Food.Nutrition.Carbohydrates / recipe.ServingsSize;
                recipe.Nutrition.Proteins += ingredient.Food.Nutrition.Proteins / recipe.ServingsSize;
                recipe.Nutrition.Fats += ingredient.Food.Nutrition.Fats / recipe.ServingsSize;
            }
            recipe.CaloriesPerServing = recipe.Nutrition.Calories / recipe.ServingsSize;
        }

        public async Task<IEnumerable<RecipeListingViewModel>> GetAllById(string? userId)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.UserId == userId)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .ThenInclude(f => f.Nutrition)
                 .ToListAsync();

            return recipe.Select(r => new RecipeListingViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                ServingsSize = r.ServingsSize,
                Unit = (int)r.Unit,
                TotalCalories = r.Nutrition.Calories,
                CaloriesPerPortion = r.CaloriesPerServing
            });
        }

        public async Task<DetailsViewModel> GetByIdAsync(string id)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.Id == id)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .FirstOrDefaultAsync();

            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                TotalCalories = recipe.Nutrition.Calories,
                Carbs = recipe.Nutrition.Carbohydrates,
                Protein = recipe.Nutrition.Proteins,
                Fats = recipe.Nutrition.Fats,
                Unit = (int)recipe.Unit,
                CaloriesPerPortion = recipe.CaloriesPerServing,
                isFinished = recipe.isFinished,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }

        public async Task<IEnumerable<IngredientDetailsViewModel>> GetIngredientsAsync(string id)
        {
            var recipe = await repo.All<Recipe>()
                .Where(r => r.Id == id)
                .Include(r => r.Nutrition)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Food)
                .ThenInclude(f => f.Nutrition)
                .FirstOrDefaultAsync();

            return recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
            {
                Id = i.Id,
                Name = i.Food.Name,
                Amount = i.Amount
            });
        }

        public async Task RemoveIngredient(string recipeid, int ingredientToRemove)
        {
            var recipie = await repo.All<Recipe>()
                 .Where(r => r.Id == recipeid)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .ThenInclude(f => f.Nutrition)
                 .FirstOrDefaultAsync();

            var ingredient = recipie.Ingredients.FirstOrDefault(i => i.Id == ingredientToRemove);

            recipie.Nutrition.Calories -= (ingredient.Food.Nutrition.Calories * ingredient.Amount) / recipie.ServingsSize;
            recipie.Nutrition.Carbohydrates -= (ingredient.Food.Nutrition.Carbohydrates * ingredient.Amount) / recipie.ServingsSize;
            recipie.Nutrition.Proteins -= (ingredient.Food.Nutrition.Proteins * ingredient.Amount) / recipie.ServingsSize;
            recipie.Nutrition.Fats -= (ingredient.Food.Nutrition.Fats * ingredient.Amount) / recipie.ServingsSize;
            recipie.CaloriesPerServing = recipie.Nutrition.Calories;

            recipie.Ingredients.Remove(ingredient);

            await repo.SaveChangesAsync();

        }
    }
}
