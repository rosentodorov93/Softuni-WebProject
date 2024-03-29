﻿using FitnessDiary.Core.Contracts;
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
        /// <summary>
        /// Creates new recipe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddAsync(CreateRecipeModel model)
        {
            var ingredients = new List<Ingredient>();
            var resultMessage = $"Successfully added recipe {model.Name}";

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
                ImageUrl = model.ImageUrl,
                Ingredients = ingredients,
                CaloriesPerServing = calories,
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

            return resultMessage;

        }
        /// <summary>
        /// Ads more ingredients to recipe
        /// </summary>
        /// <param name="ingredient"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public async Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, string recipeId)
        {
            var food = await repo.All<Food>()
                .Where(f => f.Id == ingredient.FoodId)
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync();

            var recipe = await LoadFullRecipe(recipeId);

            if (!recipe.Ingredients.Any(i => i.FoodId == food.Id))
            {
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
            }

            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                ImageUrl = recipe.ImageUrl,
                Carbs = recipe.Nutrition.Carbohydrates,
                Protein = recipe.Nutrition.Proteins,
                Fats = recipe.Nutrition.Fats,
                CaloriesPerPortion = recipe.Nutrition.Calories,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }
        /// <summary>
        /// Edits ingredients amout in recipe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DetailsViewModel> EditAsync(EditViewModel model)
        {
            var recipe = await LoadFullRecipe(model.Id);


            recipe.Name = model.Name;
            recipe.ServingsSize = model.ServingsSize;
            recipe.ImageUrl = model.ImageUrl;
            recipe.Nutrition.Calories = 0;
            recipe.Nutrition.Carbohydrates = 0;
            recipe.Nutrition.Proteins = 0;
            recipe.Nutrition.Fats = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                var newIngredientAmount = model.Ingredients.Where(i => i.Id == ingredient.Id).FirstOrDefault()?.Amount ?? -1;
                if (newIngredientAmount != -1)
                {
                    ingredient.Amount = newIngredientAmount;
                }

            }

            CalculateNutrition(recipe);

            await repo.SaveChangesAsync();

            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                ImageUrl = recipe.ImageUrl,
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

        private void CalculateNutrition(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                recipe.Nutrition.Calories += (ingredient.Food.Nutrition.Calories * ingredient.Amount) / recipe.ServingsSize;
                recipe.Nutrition.Carbohydrates += (ingredient.Food.Nutrition.Carbohydrates * ingredient.Amount) / recipe.ServingsSize;
                recipe.Nutrition.Proteins += (ingredient.Food.Nutrition.Proteins * ingredient.Amount) / recipe.ServingsSize;
                recipe.Nutrition.Fats += (ingredient.Food.Nutrition.Fats * ingredient.Amount) / recipe.ServingsSize;
            }
            recipe.CaloriesPerServing = recipe.Nutrition.Calories;
        }
        /// <summary>
        /// Returns all recipes by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RecipeListingViewModel>> GetAllByUserId(string userId)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.IsActive)
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
                ImageUrl = r.ImageUrl,
                CaloriesPerPortion = r.CaloriesPerServing
            });
        }
        /// <summary>
        /// Returns recipe details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<DetailsViewModel> GetDetailsByIdAsync(string id)
        {
            var recipe = await LoadFullRecipe(id);

            if (recipe == null)
            {
                throw new ArgumentException("Invalid recipe Id");
            }

            return new DetailsViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                ImageUrl = recipe.ImageUrl,
                Carbs = recipe.Nutrition.Carbohydrates,
                Protein = recipe.Nutrition.Proteins,
                Fats = recipe.Nutrition.Fats,
                CaloriesPerPortion = recipe.Nutrition.Calories,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }
        /// <summary>
        /// Returns recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditViewModel> GetByIdAsync(string id)
        {
            var recipe = await repo.All<Recipe>()
                 .Where(r => r.IsActive)
                 .Where(r => r.Id == id)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .FirstOrDefaultAsync();

            return new EditViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                ImageUrl = recipe.ImageUrl,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Food.Name,
                    Amount = i.Amount
                }).ToList()
            };
        }
        /// <summary>
        /// Returns all ingredients in recie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IngredientDetailsViewModel>> GetIngredientsAsync(string id)
        {
            var recipe = await LoadFullRecipe(id);

            return recipe.Ingredients.Select(i => new IngredientDetailsViewModel()
            {
                Id = i.Id,
                Name = i.Food.Name,
                Amount = i.Amount
            });
        }
        /// <summary>
        /// Removes ingredient from recipe
        /// </summary>
        /// <param name="recipeid"></param>
        /// <param name="ingredientToRemove"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task RemoveIngredient(string recipeid, int ingredientToRemove)
        {
            var recipe = await LoadFullRecipe(recipeid);


            var ingredient = recipe.Ingredients.FirstOrDefault(i => i.Id == ingredientToRemove);

            if (ingredient == null)
            {
                throw new ArgumentException("Invalid Ingredient");
            }

            recipe.Nutrition.Calories -= (ingredient.Food.Nutrition.Calories * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Carbohydrates -= (ingredient.Food.Nutrition.Carbohydrates * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Proteins -= (ingredient.Food.Nutrition.Proteins * ingredient.Amount) / recipe.ServingsSize;
            recipe.Nutrition.Fats -= (ingredient.Food.Nutrition.Fats * ingredient.Amount) / recipe.ServingsSize;
            recipe.CaloriesPerServing = recipe.Nutrition.Calories;

            recipe.Ingredients.Remove(ingredient);

            await repo.SaveChangesAsync();

        }
        /// <summary>
        /// Chech if recipe exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await repo.AllReadonly<Recipe>().Where(r => r.IsActive).AnyAsync(r => r.Id == id);
        }
        /// <summary>
        /// Deletes recipe 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string id)
        {
            var resultMessage = "Error! Unable to delete item";

            var recipe = await repo.All<Recipe>()
                 .Where(r => r.IsActive)
                 .FirstOrDefaultAsync(f => f.Id == id);

            if (recipe != null)
            {
                recipe.IsActive = false;
                resultMessage = "Successfuly deleted item";
                await repo.SaveChangesAsync();
            }

            return resultMessage;
        }

        private async Task<Recipe> LoadFullRecipe(string id)
        {
            return await repo.All<Recipe>()
                 .Where(r => r.IsActive)
                 .Where(r => r.Id == id)
                 .Include(r => r.Nutrition)
                 .Include(r => r.Ingredients)
                 .ThenInclude(i => i.Food)
                 .ThenInclude(f => f.Nutrition)
                 .FirstOrDefaultAsync();
        }
    }
}
