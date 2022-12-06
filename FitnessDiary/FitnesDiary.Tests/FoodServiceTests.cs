using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnesDiary.Tests
{
    public class FoodServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder;
        private ApplicationDbContext context;
        private IRepository repo;
        private IFoodService foodService;
        [SetUp]
        public void Setup()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "FoodsInMemoryDb");
            this.context = new ApplicationDbContext(optionsBuilder.Options);
            this.repo = new Repository(this.context);
            this.foodService = new FoodService(repo);

        }

        [Test]
        public async Task AddAsyncShouldAddValidFood()
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
                Fats = 0.1
            };
            //Act
            await this.foodService.AddFood(food, "2611c647-2f9c-45af-96c5-d2a64a424266");

            var result = await this.repo.All<Food>().Include(f => f.Nutrition).Where(f => f.Name == "Apple" && f.Type == "Fruid").FirstOrDefaultAsync();

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, food.Name);
            Assert.AreEqual(result.Type, food.Type);
            Assert.AreEqual(result.Nutrition.Calories, food.Calories);
            Assert.AreEqual(result.Nutrition.Carbohydrates, food.Carbohydtrates);
            Assert.AreEqual(result.Nutrition.Proteins, food.Proteins);
            Assert.AreEqual(result.Nutrition.Fats, food.Fats);

        }
        [Test]
        public async Task ExistsByIdShouldReturnTrueIfIdExists()
        {
            LoadData();
            var result = await foodService.ExistsByIdAsync("test");

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsByIdShouldReturnFalseIfIdExists()
        {
            LoadData();
            var result = await foodService.ExistsByIdAsync("invalid");

            Assert.IsFalse(result);
        }
        [Test]
        public async Task EditAsyncShoudEditFoodCorrectly()
        {
            LoadData();
            var edited = new FoodViewModel()
            {
                Name = "Egg Edited",
                Type = "Protein edited",
                MeassureUnit = 2,
                Calories = 120,
                Carbohydtrates = 21,
                Proteins = 10,
                Fats = 3
            };

            await this.foodService.EditAsync("test", edited);
            var result = await this.repo.AllReadonly<Food>().Include(f => f.Nutrition).FirstOrDefaultAsync(f => f.Id == "test");

            //Asert
            Assert.AreEqual(result.Name, edited.Name);
            Assert.AreEqual(result.Type, edited.Type);
            Assert.AreEqual(result.Nutrition.Calories, edited.Calories);
            Assert.AreEqual(result.Nutrition.Carbohydrates, edited.Carbohydtrates);
            Assert.AreEqual(result.Nutrition.Proteins, edited.Proteins);
            Assert.AreEqual(result.Nutrition.Fats, edited.Fats);
        }
        [Test]
        public async Task EditAsyncShoudNotEditIfIdIsInvalid()
        {
            LoadData();
            var edited = new FoodViewModel()
            {
                Name = "Egg Edited",
                Type = "Protein edited",
                MeassureUnit = 2,
                Calories = 120,
                Carbohydtrates = 21,
                Proteins = 10,
                Fats = 3
            };

            await this.foodService.EditAsync("invalid", edited);
            var result = await this.repo.All<Food>().Include(f => f.Nutrition).Where(f => f.Id == "test").FirstOrDefaultAsync();

            //Asert
            Assert.AreNotEqual(result.Name, edited.Name);
            Assert.AreNotEqual(result.Type, edited.Type);
            Assert.AreNotEqual(result.Nutrition.Calories, edited.Calories);
            Assert.AreNotEqual(result.Nutrition.Carbohydrates, edited.Carbohydtrates);
            Assert.AreNotEqual(result.Nutrition.Proteins, edited.Proteins);
            Assert.AreNotEqual(result.Nutrition.Fats, edited.Fats);
        }
        [Test]
        public async Task GetByIdShouldReturnCorrectFood()
        {
            LoadData();
            FoodViewModel result = await foodService.GetByIdAsync("test");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Egg Size M");
        }
        [Test]
        public async Task DeleteShouldDeleteItemWithCorrectId()
        {
            LoadData();

            await foodService.DeleteAsync("test");

            var result = await repo.AllReadonly<Food>().FirstOrDefaultAsync(f => f.Id == "test");

            Assert.IsFalse(result.IsActive);
        }
        [Test]
        public async Task DeleteShoulNotDeleteItemWithInvalidId()
        {
            LoadData();

            await foodService.DeleteAsync("invalid");

            var result = await repo.AllReadonly<Food>().FirstOrDefaultAsync(f => f.Id == "test");

            Assert.IsTrue(result.IsActive);
        }

        [Test]
        public async Task GetAllTypesAsyncReturnsCorrectData()
        {
            LoadData();
            var types = new List<string>(){ "Vegetables", "Fruit", "Protein" };

            var result = await foodService.getAllTypesAsync();

            

            Assert.AreEqual(types.Count, result.Count());
            CollectionAssert.AreEqual(types, result);

        }
        [Test]
        public async Task LoadIngredientsReturnsCorrectData()
        {
            LoadData();

            var food = new FoodViewModel()
            {
                Name = "Apple",
                Type = "Fruid",
                MeassureUnit = 2,
                Calories = 100,
                Carbohydtrates = 12,
                Proteins = 1,
                Fats = 0.1
            };
            //Act
            await this.foodService.AddFood(food, "2611c647-2f9c-45af-96c5-d2a64a424266");

            var result = await this.foodService.LoadIngedientsAsync();

            Assert.AreEqual(result.Count(), 4);

            await this.foodService.DeleteAsync("test");
            var resultAfterDelete = await this.foodService.LoadIngedientsAsync();

            Assert.AreEqual(resultAfterDelete.Count(), 3);

        }
        [Test]
        public async Task GetAllAsyncShouldReturnFoodsWithNoUserWhenUserIdIsNull()
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync();

            Assert.AreEqual(result.TotalFoodsCount, 3);
        }
        [Test]
        public async Task GetAllAsyncShouldReturnFoodsWithUserWhenUserIdIsValid()
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync("user");

            Assert.AreEqual(result.TotalFoodsCount, 4);
        }
        [Test]
        [TestCase("p")]
        public async Task GetAllAsyncShouldReturnCorrectFoodWithUserAndSearchParams(string search)
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync("user", null, search);

            Assert.AreEqual(result.TotalFoodsCount, 3);
        }
        [Test]
        [TestCase("Fruit")]
        [TestCase("Vegetable")]
        [TestCase("Grains")]
        public async Task GetAllAsyncShouldReturnCorrectFoodWithUserAndTypeParam(string type)
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync("user", type, null);

            Assert.AreEqual(result.TotalFoodsCount, 1);
        }
        [Test]
        [TestCase("ba")]
        [TestCase("eg")]
        [TestCase("po")]
        public async Task GetAllAsyncShouldReturnCorrectFoodWithNoUserAndSearchParam(string search)
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync(null, null, search);

            Assert.AreEqual(result.TotalFoodsCount, 1);
        }
        [Test]
        [TestCase("dada")]
        [TestCase("test")]
        [TestCase("invalid")]
        public async Task GetAllAsyncShouldReturnNotReturnresultWithIncorectSarch(string search)
        {
            LoadData();
            LoadMineFoods();

            var result = await this.foodService.GetAllAsync(null, null, search);

            Assert.AreEqual(result.TotalFoodsCount, 0);
        }
        private void LoadData()
        {
            Clear();
            var foods = new List<Food>()
            {
                new Food()
                {
                    Id = "test",
                    Name = "Egg Size M",
                    MeassureUnits = MeassureUnitType.CaloriesPerItem,
                    Type = "Protein",
                    NutritionId = 3,
                    IsActive = true,
                },
                new Food()
                {
                    Id = "8070aa93-ea4c-477e-972b-aa3370f2d701",
                    Name = "Banana",
                    MeassureUnits = MeassureUnitType.CaloriesPerItem,
                    Type = "Fruit",
                    NutritionId = 4,
                    IsActive = true,
                    
                },
                new Food()
                {
                    Id = "7bbc16e1-faa6-46ad-90ba-3dc038105ea2",
                    Name = "Potato",
                    MeassureUnits = MeassureUnitType.CaloriesPer100grams,
                    Type = "Vegetables",
                    NutritionId = 5,
                    IsActive = true,
                },
            };
            var nutritions = new List<NutritionData>()
           {

               new NutritionData()
                    {
                        Id = 3,
                        Calories = 66,
                        Carbohydrates = 0.3,
                        Proteins = 6.4,
                        Fats = 4.6
                    },
               new NutritionData()
                    {
                        Id = 4,
                        Calories = 89,
                        Carbohydrates = 23,
                        Proteins = 1,
                        Fats = 0.3
                    },
                new NutritionData()
                    {
                        Id = 5,
                        Calories = 77,
                        Carbohydrates = 17,
                        Proteins = 2,
                        Fats = 0.1
                    }
           };

            context.Nutritions.AddRange(nutritions);
            context.Foods.AddRange(foods);
            context.SaveChanges();
        }
        private void Clear()
        {
            foreach (var nutrition in context.Nutritions)
            {
                context.Nutritions.Remove(nutrition);
            }
            foreach (var food in context.Foods)
            {
                context.Foods.Remove(food);
            }

            context.SaveChanges();
        }

        private void LoadMineFoods()
        {
            
            var foods = new List<Food>()
            {
                new Food()
                {
                    Id = "my1",
                    Name = "Rise",
                    MeassureUnits = MeassureUnitType.CaloriesPerItem,
                    Type = "Grains",
                    NutritionId = 6,
                    IsActive = true,
                    UserId = "user"
                },
                new Food()
                {
                    Id = "my2",
                    Name = "Peach",
                    MeassureUnits = MeassureUnitType.CaloriesPerItem,
                    Type = "Fruit",
                    NutritionId = 7,
                    IsActive = true,
                    UserId = "user"

                },
                new Food()
                {
                    Id = "my3",
                    Name = "Peanuts",
                    MeassureUnits = MeassureUnitType.CaloriesPer100grams,
                    Type = "Nuts",
                    NutritionId = 8,
                    IsActive = true,
                    UserId = "user"
                },
                new Food()
                {
                    Id = "my4",
                    Name = "Potatoes",
                    MeassureUnits = MeassureUnitType.CaloriesPerItem,
                    Type = "Vegetable",
                    NutritionId = 9,
                    IsActive = true,
                    UserId = "user"
                },
            };
            var nutritions = new List<NutritionData>()
           {

               new NutritionData()
                    {
                        Id = 6,
                        Calories = 66,
                        Carbohydrates = 0.3,
                        Proteins = 6.4,
                        Fats = 4.6
                    },
               new NutritionData()
                    {
                        Id = 7,
                        Calories = 89,
                        Carbohydrates = 23,
                        Proteins = 1,
                        Fats = 0.3
                    },
                new NutritionData()
                    {
                        Id = 8,
                        Calories = 77,
                        Carbohydrates = 17,
                        Proteins = 2,
                        Fats = 0.1
                    },
                new NutritionData()
                    {
                        Id = 9,
                        Calories = 77,
                        Carbohydrates = 17,
                        Proteins = 2,
                        Fats = 0.1
                    }
            };

            context.Nutritions.AddRange(nutritions);
            context.Foods.AddRange(foods);
            context.SaveChanges();
        }
    }
}