﻿using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FitnesDiary.Tests.UnitTests
{
    [TestFixture]
    public class RecipeServiceTests : UnitTestsBase
    {
        private IRecipeService recipeService;
        private IRepository repo;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.repo = new Repository(this.data);
            this.recipeService = new RecipeService(repo);
        }

        [Test]
        public async Task AddAsynnc_ShouldAddRecipeCorrectly()
        {
            var recipeModel = new CreateRecipeModel()
            {
                Name = "Lasagnia",
                ServingsSize = 8,
                ImageUrl = "pastaImageUrl",
                UserId = this.AppUser.Id,
                Ingredients = new List<IngredientViewModel>()
                {
                    new IngredientViewModel()
                    {
                        FoodId = this.TestFood.Id,
                        Amount = 2
                    }
                },

            };

            var recipeCountBeforeAdd = data.Recipes.Where(r => r.IsActive).Count();

            await recipeService.AddAsync(recipeModel);
            var result = repo.AllReadonly<Recipe>().Include(r => r.Nutrition).FirstOrDefault(r => r.Name == recipeModel.Name);
            var recipeCountAfterAdd = data.Recipes.Where(r => r.IsActive).Count();

            Assert.That(recipeCountAfterAdd,Is.EqualTo(recipeCountBeforeAdd + 1));
            Assert.That(result.Name,Is.EqualTo(recipeModel.Name));
            Assert.That(result.ServingsSize,Is.EqualTo(recipeModel.ServingsSize) );
            Assert.That(result.ImageUrl, Is.EqualTo(recipeModel.ImageUrl));
            Assert.That(result.Nutrition.Calories,Is.EqualTo( ((this.TestFood.Nutrition.Calories * 2) / recipeModel.ServingsSize)));
            Assert.That(result.Nutrition.Carbohydrates,Is.EqualTo( ((this.TestFood.Nutrition.Carbohydrates * 2) / recipeModel.ServingsSize)));
            Assert.That(result.Nutrition.Proteins,Is.EqualTo( ((this.TestFood.Nutrition.Proteins * 2) / recipeModel.ServingsSize)));
            Assert.That(result.Nutrition.Fats,Is.EqualTo( ((this.TestFood.Nutrition.Fats * 2) / recipeModel.ServingsSize)));


        }
        [Test]
        public async Task AddIngredientAsync_ShouldAddCorrectly()
        {
            var food = data.Foods.Where(r => r.IsActive).Include(f => f.Nutrition).FirstOrDefault(f => f.Id == "eggId");
            var caloriesBeforeAdd = this.TestRecipe.Nutrition.Calories;

            var ingredientToAdd = new IngredientViewModel()
            {
                Amount = 1,
                FoodId = food.Id,
            };

            await recipeService.AddIngredientAsync(ingredientToAdd, this.TestRecipe.Id);

            Assert.That(this.TestRecipe.Ingredients.Count, Is.EqualTo(2));
            
        }
        [Test]
        public async Task AddIngredientAsync_ShouldNotAddIngredientIfItIsAlreadyInRecipe()
        {
            var ingredientToAdd = new IngredientViewModel()
            {
                Amount = 1,
                FoodId = "foodId",
            };

            await recipeService.AddIngredientAsync(ingredientToAdd, this.TestRecipe.Id);

            Assert.That(this.TestRecipe.Ingredients.Count,Is.EqualTo(2));
        }
        [Test]
        public async Task RemoveIngredient_ShouldRemoveWithCorrectId()
        {
            var ingredientToRemove = 3;
        

            await recipeService.RemoveIngredient(this.TestRecipe.Id, ingredientToRemove);

            Assert.That(this.TestRecipe.Ingredients.Count, Is.EqualTo(1));
            
        }
        [Test]
        public async Task RemoveIngredient_ShouldNotRemoveWithInvalidId()
        {
            var ingredientToRemove = 33;

            Assert.ThrowsAsync<ArgumentException>(async () => await recipeService.RemoveIngredient(this.TestRecipe.Id, ingredientToRemove));
        }
        [Test]
        public async Task GetAllById_ShouldReturnCorrectRecipes()
        {
            var userRecipesCount = this.AppUser.Recipes.Where(r => r.IsActive).Count();
            var recipesNames = this.AppUser.Recipes.Select(r => r.Name).ToList();

            var result = await recipeService.GetAllByUserId(AppUser.Id);
            var resultRecipeNames = result.Select(r => r.Name).ToList();

            Assert.That(result.Count, Is.EqualTo(userRecipesCount));
            Assert.IsTrue(result.Any(r => r.Name == this.TestRecipe.Name));
            Assert.That(recipesNames, Is.SameAs(recipesNames));
        }
        [Test]
        public async Task LoadIngredients_ShouldReturnCorrectIngredientsWithValidRecipeId()
        {
            var ingredientsCount = this.TestRecipe.Ingredients.Count();
            var ingredientsNames = string.Join(", ", this.TestRecipe.Ingredients.Select(i => i.Food.Name.Trim()).ToList());

            var result = await recipeService.GetIngredientsAsync(this.TestRecipe.Id);
            var resultingredientNames = string.Join(", ", result.Select(r => r.Name.Trim()).ToList());

            Assert.That(result.Count, Is.EqualTo(ingredientsCount));
            Assert.That(ingredientsNames, Is.EqualTo(resultingredientNames));
        }
       
        [Test]
        public async Task EditAsync_ShouldEditCorrectly()
        {
            var newIngredientAmount = 3;
            var ingredientIds = this.TestRecipe.Ingredients.Select(i => i.Id).ToList();
            var editModel = new EditViewModel()
            {
                Id = this.TestRecipe.Id,
                Name = "Edited name",
                ServingsSize = 6,
                ImageUrl = "imgEdited",
                Ingredients = new List<IngredientDetailsViewModel>(),

            };

            foreach (var id in ingredientIds)
            {
                var ingredient = new IngredientDetailsViewModel()
                {
                    Id = id,
                    Amount = newIngredientAmount

                };

                editModel.Ingredients.Add(ingredient);
            }

            var result = await recipeService.EditAsync(editModel);

            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(editModel.Name));
            Assert.That(result.ServingsSize.Equals(editModel.ServingsSize));
            Assert.That(result.ImageUrl.Equals(editModel.ImageUrl));
            Assert.IsTrue(result.Ingredients.All(i => i.Amount == newIngredientAmount));

        }
       
        [Test]
        public async Task GetDetailsByIdAsync_ShouldThrowErrorWithInvalidId()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await recipeService.GetDetailsByIdAsync("invalidId"));
        }
        [Test]
        public async Task GetDetailsByIdAsync_ShouldReturnCorrectRecipe()
        {
            var result = await recipeService.GetDetailsByIdAsync(this.TestRecipe.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(this.TestRecipe.Name));
            Assert.That(result.ServingsSize.Equals(this.TestRecipe.ServingsSize));
            Assert.That(result.ImageUrl.Equals(this.TestRecipe.ImageUrl));
        }
        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectRecipe()
        {
            var result = await recipeService.GetByIdAsync(this.TestRecipe.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(this.TestRecipe.Name));
            Assert.That(result.ServingsSize.Equals(this.TestRecipe.ServingsSize));
            Assert.That(result.ImageUrl.Equals(this.TestRecipe.ImageUrl));
        }
        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrueWithValidId()
        {
            var result = await recipeService.ExistsByIdAsync(this.TestRecipe.Id);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsByIdAsync_ShouldReturnFalseWithInvalidId()
        {
            var result = await recipeService.ExistsByIdAsync("invalid");

            Assert.IsFalse(result);
        }
        [Test]
        public async Task DeleteAsync_ShouldDeleteCorrectly()
        {
            var recipeToDelete = data.Recipes.Where(r => r.IsActive).FirstOrDefault(r => r.Name == "Lasagnia");

            await recipeService.DeleteAsync(recipeToDelete.Id);

            Assert.IsFalse(recipeToDelete.IsActive);
        }
    }
}
