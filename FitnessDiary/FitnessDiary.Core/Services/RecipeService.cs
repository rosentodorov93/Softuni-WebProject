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
    public class RecipeService: IRecipeService
    {
        private readonly IRepository repo;

        public RecipeService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> AddAsync(CreateViewModel model)
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

        public async Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, int recepieId)
        {
            var food = await repo.All<Food>()
                .Where(f => f.Id == ingredient.FoodId)
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync();

            var recipe = await repo.All<Recipe>()
                .Where(r => r.Id == recepieId)
                .Include(r => r.Nutrition)
                .ThenInclude(r => r.Foods)
                .FirstOrDefaultAsync();

            recipe.Foods.Add(food);

            recipe.Nutrition.Calories += food.Nutrition.Calories * ingredient.Amount;
            recipe.Nutrition.Carbohydrates += food.Nutrition.Carbohydrates * ingredient.Amount;
            recipe.Nutrition.Proteins += food.Nutrition.Proteins * ingredient.Amount;
            recipe.Nutrition.Fats += food.Nutrition.Fats * ingredient.Amount;

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
                isFinished = recipe.isFinished
            };
        }

        public async Task<IEnumerable<RecipeListingViewModel>> GetAllById(string? userId)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.UserId == userId)
                 .Include(r => r.Nutrition)
                 .ThenInclude(r => r.Foods)
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

        public async Task<DetailsViewModel> GetByIdAsync(int id)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.Id == id)
                 .Include(r => r.Nutrition)
                 .ThenInclude(r => r.Foods)
                 .FirstOrDefaultAsync();

            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                Unit = (int)recipe.Unit,
                CaloriesPerPortion = recipe.CaloriesPerServing,
                TotalCalories = recipe.Nutrition.Calories,
                Carbs = recipe.Nutrition.Carbohydrates,
                Protein = recipe.Nutrition.Proteins,
                Fats = recipe.Nutrition.Fats,
                isFinished = recipe.isFinished
            };
        }
    }
}
