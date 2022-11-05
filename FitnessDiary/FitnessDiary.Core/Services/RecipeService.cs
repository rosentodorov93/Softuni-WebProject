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

        public async Task AddAsync(CreateViewModel model)
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
    }
}
