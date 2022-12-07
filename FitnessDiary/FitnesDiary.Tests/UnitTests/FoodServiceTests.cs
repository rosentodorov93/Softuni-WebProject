using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesDiary.Tests.UnitTests
{
    public class FoodServiceTests: UnitTestsBase
    {
        private IFoodService foodService;
        private IRepository repo;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.repo = new Repository(this.data);
            this.foodService = new FoodService(repo);
        }
        [Test]
        public async Task AddAsync_ShouldAddValidFoodWithUserId()
        {
            //Arrange
            var food = new FoodViewModel()
            {
                Name = "Apple",
                Type = "Fruid",
                MeassureUnit = 2,
                Calories = 100,
                Carbohydtrates = 12,
                Proteins = 1,
                Fats = 0.1,
                
            };
            var userFoodsBeforeAdd = this.AppUser.Foods.Count;
            //Act
            await this.foodService.AddFood(food, this.AppUser.Id);
            var userFoodsAfterAdd = this.AppUser.Foods.Count;

            //Asert
            Assert.That(userFoodsAfterAdd.Equals(userFoodsBeforeAdd + 1));
            Assert.IsTrue(this.AppUser.Foods.Any(f => f.Name == food.Name && f.Type == food.Type));

        }
        [Test]
        public async Task AddAsync_ShouldAddValidFoodWithoutUserId()
        {
            //Arrange
            var food = new FoodViewModel()
            {
                Name = "Chicken",
                Type = "Meat",
                MeassureUnit = 1,
                Calories = 112,
                Carbohydtrates = 4,
                Proteins = 21,
                Fats = 3,

            };
            var userFoodsBeforeAdd = this.AppUser.Foods.Count;
            //Act
            await this.foodService.AddFood(food, null);
            var userFoodsAfterAdd = this.AppUser.Foods.Count;

            //Asert
            Assert.That(userFoodsAfterAdd.Equals(userFoodsBeforeAdd));
            Assert.IsTrue(repo.AllReadonly<Food>().Any(f => f.Name == food.Name && f.Type == food.Type));

        }
        [Test]
        public async Task ExistsById_ShouldReturnTrueIfIdExists()
        {
            var result = await foodService.ExistsByIdAsync(this.TestFood.Id);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsById_ShouldReturnFalseIfIdExists()
        {
            var result = await foodService.ExistsByIdAsync("invalid");

            Assert.IsFalse(result);
        }
        [Test]
        public async Task EditAsync_ShoudEditFoodCorrectly()
        {
            var edited = new FoodViewModel()
            {
                Name = "Edited",
                Type = "Edited",
                MeassureUnit = 2,
                Calories = 120,
                Carbohydtrates = 21,
                Proteins = 10,
                Fats = 3
            };

            await this.foodService.EditAsync(this.TestFood.Id, edited);


            //Asert
            Assert.That(this.TestFood.Name.Equals(edited.Name));
            Assert.That(this.TestFood.Type.Equals(edited.Type));
            Assert.That(this.TestFood.Nutrition.Calories.Equals(edited.Calories));
            Assert.That(this.TestFood.Nutrition.Carbohydrates.Equals(edited.Carbohydtrates));
            Assert.That(this.TestFood.Nutrition.Proteins.Equals(edited.Proteins));
            Assert.That(this.TestFood.Nutrition.Fats.Equals(edited.Fats));
        }
        [Test]
        public async Task EditAsync_ShoudNotEditIfIdIsInvalid()
        {
            var edited = new FoodViewModel()
            {
                Name = "Not Edit",
                Type = "Not Edit",
                MeassureUnit = 2,
                Calories = 123,
                Carbohydtrates = 23,
                Proteins = 12,
                Fats = 4
            };

            await this.foodService.EditAsync("invalid", edited);


            //Asert
            Assert.AreNotEqual(this.TestFood.Name, edited.Name);
            Assert.AreNotEqual(this.TestFood.Type, edited.Type);
            Assert.AreNotEqual(this.TestFood.Nutrition.Calories, edited.Calories);
            Assert.AreNotEqual(this.TestFood.Nutrition.Carbohydrates, edited.Carbohydtrates);
            Assert.AreNotEqual(this.TestFood.Nutrition.Proteins, edited.Proteins);
            Assert.AreNotEqual(this.TestFood.Nutrition.Fats, edited.Fats);
        }
        [Test]
        public async Task GetById_ShouldReturnCorrectFood()
        {
            FoodViewModel result = await foodService.GetByIdAsync(this.TestFood.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(this.TestFood.Name));
        }
        [Test]
        public async Task Delete_ShouldDeleteItemWithCorrectId()
        {
            var foodCountBeforeDelete = repo.AllReadonly<Food>().Where(f => f.IsActive).Count();
            var food = repo.All<Food>().Last();

            await foodService.DeleteAsync(food.Id);
            var foodCountAfterDelete = repo.AllReadonly<Food>().Where(f => f.IsActive).Count();

            Assert.IsFalse(food.IsActive);
            Assert.That(foodCountAfterDelete.Equals(foodCountBeforeDelete - 1));
        }
        [Test]
        public async Task Delete_ShoulNotDeleteItemWithInvalidId()
        {
            var foodCountBeforeDelete = repo.AllReadonly<Food>().Where(f => f.IsActive).Count();

            await foodService.DeleteAsync("invalid");
            var foodCountAfterDelete = repo.AllReadonly<Food>().Where(f => f.IsActive).Count();


            Assert.That(foodCountAfterDelete.Equals(foodCountBeforeDelete));
        }
        [Test]
        public async Task GetAllTypesAsync_ShouldReturnsCorrectData()
        {
            var types = repo.AllReadonly<Food>().Where(f => f.IsActive).Select(f => f.Type).ToList();


            var result = await foodService.getAllTypesAsync();

            CollectionAssert.AreEqual(types, result);

        }
        [Test]
        public async Task LoadIngredients_ShouldReturnsCorrectData()
        {
            var foods = repo.All<Food>().Where(f => f.IsActive).Include(f => f.Nutrition).ToList();

            var result = await this.foodService.LoadIngedientsAsync();

            Assert.That(result.Count().Equals(foods.Count()));

        }
        [Test]
        public async Task GetAllAsync_ShouldReturnFoodsWithNoUserWhenUserIdIsNull()
        {
            var expectedFoods = repo.AllReadonly<Food>().Where(f => f.IsActive).Where(f => f.UserId == null).ToList();

            var result = await this.foodService.GetAllAsync();

            Assert.That(result.TotalFoodsCount.Equals(expectedFoods.Count));
        }
        [Test]
        public async Task GetAllAsync_ShouldReturnFoodsWithUserWhenUserIdIsValid()
        {
            var expectedFoods = repo.AllReadonly<Food>().Where(f => f.IsActive).Where(f => f.UserId != null).ToList();

            var result = await this.foodService.GetAllAsync(this.AppUser.Id);

            Assert.That(result.TotalFoodsCount.Equals(expectedFoods.Count));
        }
        [Test]
        [TestCase("p")]
        public async Task GetAllAsyncShouldReturnCorrectFoodWithUserAndSearchParams(string search)
        {
            var expectedFoods = repo.AllReadonly<Food>()
                .Where(f => f.IsActive)
                .Where(f => f.UserId != null)
                .Where(f => f.Name.ToLower().Contains(search))
                .ToList();

            

            var result = await this.foodService.GetAllAsync(this.AppUser.Id, null, search);

            Assert.That(result.TotalFoodsCount.Equals(expectedFoods.Count));
        }
    }
}
