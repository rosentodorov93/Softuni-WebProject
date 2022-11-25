using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository repo;

        public RecipeService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAsync(CreateRecipeModel model)
        {
            var ingredients = new List<Ingredient>();

            double calories = 0;
            double carbs = 0;
            double proteins = 0;
            double fats = 0;

            foreach (var item in model.Ingredients)
            {
                var food = await repo.AllReadonly<Food>().Include(f => f.Nutrition).Where(f => f.IsActive).FirstOrDefaultAsync(f => f.Id == item.FoodId);

                if (food != null)
                {
                    var ingredient = new Ingredient()
                    {
                        FoodId = food.Id,
                        Amount = item.Amount,
                    };

                    ingredients.Add(ingredient);

                    calories += (food.Nutrition.Calories * item.Amount) / model.ServingsSize;
                    carbs += (food.Nutrition.Carbohydrates * item.Amount) / model.ServingsSize;
                    proteins += (food.Nutrition.Proteins * item.Amount) / model.ServingsSize;
                    fats += (food.Nutrition.Fats * item.Amount) / model.ServingsSize;
                }
            }

            var recipe = new Recipe()
            {
                Name = model.Name,
                ServingsSize = model.ServingsSize,
                UserId = model.UserId,
                Ingredients = ingredients,
                CaloriesPerServing = 1000,
                Nutrition = new NutritionData()
                {
                    Calories = calories,
                    Carbohydrates = carbs,
                    Proteins = proteins,
                    Fats = fats
                },
            };

            await repo.AddAsync<Recipe>(recipe);
            await repo.SaveChangesAsync();

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

            recipe?.Ingredients.Add(new Ingredient()
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
            recipe.Nutrition.Calories = 0;
            recipe.Nutrition.Carbohydrates = 0;
            recipe.Nutrition.Proteins = 0;
            recipe.Nutrition.Fats = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                var newIngredientAmount = model.Ingredients.Where(i => i.Id == ingredient.Id).FirstOrDefault().Amount;
                ingredient.Amount = newIngredientAmount;
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
                CaloriesPerPortion = recipe.CaloriesPerServing,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }

        private  void CalculateNutrition(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                recipe.Nutrition.Calories += (ingredient.Food.Nutrition.Calories * ingredient.Amount) / recipe.ServingsSize;
                recipe.Nutrition.Carbohydrates += (ingredient.Food.Nutrition.Carbohydrates * ingredient.Amount )/ recipe.ServingsSize;
                recipe.Nutrition.Proteins += (ingredient.Food.Nutrition.Proteins * ingredient.Amount) / recipe.ServingsSize;
                recipe.Nutrition.Fats += (ingredient.Food.Nutrition.Fats * ingredient.Amount )/ recipe.ServingsSize;
            }
            recipe.CaloriesPerServing = recipe.Nutrition.Calories / recipe.ServingsSize;
        }

        public async Task<IEnumerable<RecipeListingViewModel>> GetAllById(string? userId)
        {
            var recipe = await repo.All<Recipe>()
                 //.Where(r => r.UserId == userId)
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
                CaloriesPerPortion = recipe.CaloriesPerServing,
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
