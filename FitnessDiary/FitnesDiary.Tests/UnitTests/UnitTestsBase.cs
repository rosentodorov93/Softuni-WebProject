using FitnesDiary.Tests.Mocks;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using Microsoft.AspNetCore.Identity;

namespace FitnesDiary.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext data;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.data = DatabaseMock.Instance;
            this.SeedDatabase();
        }

        public Food TestFood { get; private set; } = null!;
        public Recipe TestRecipe { get; private set; } = null!;
        public WorkoutTamplate TestTamplate { get; private set; } = null!;
        public ApplicationUser AppUser { get; private set; } = null!;
        private void SeedDatabase()
        {
            var nutritions = new List<NutritionData>()
            {
               new NutritionData()
               {
                    Id = 1,
                    Calories = 1500,
                    Carbohydrates = 200,
                    Proteins = 110,
                    Fats = 40
               },
               new NutritionData()
               {
                    Id = 2,
                    Calories = 66,
                    Carbohydrates = 12,
                    Proteins = 8,
                    Fats = 1
               },
               new NutritionData()
               {
                    Id = 3,
                    Calories = 110,
                    Carbohydrates = 28,
                    Proteins = 14,
                    Fats = 4
               },
            };

            this.data.Nutritions.AddRange(nutritions);

            var foods = new List<Food>()
            {
                new Food()
                {
                    Id = "foodId",
                    Name = "Potato",
                    MeassureUnits = FitnessDiary.Infrastructure.Data.Enums.MeassureUnitType.CaloriesPer100grams,
                    User = null,
                    Type = "vegetable",
                    IsActive = true,
                    NutritionId = 2,
                },
                new Food()
                {
                    Id = "eggId",
                    Name = "Egg",
                    MeassureUnits = FitnessDiary.Infrastructure.Data.Enums.MeassureUnitType.CaloriesPerItem,
                    User = null,
                    Type = "eggs and meat",
                    IsActive = true,
                    NutritionId = 3,
                },

            };

            this.TestFood = foods[0];
            this.data.Foods.AddRange(foods);

            var user = new IdentityUser()
            {
                Id = "userId",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "test@mail.bg",
                NormalizedEmail = "TEST@MAIL.BG"
            };

            this.data.Users.Add(user);

            var appUser = new ApplicationUser
            {
                FullName = "Test AppUser",
                UserId = "userId",
                Age = 25,
                Weight = 75,
                ActivityLevelId = 1,
                Height = 175,
                Gender = FitnessDiary.Infrastructure.Data.Enums.Gender.Male,
                FitnessGoal = FitnessDiary.Infrastructure.Data.Enums.FitnessGoalType.LoseWeight,
                NutritionId = 1,
                Id = "AppUserId",

            };

            this.AppUser = appUser;

            this.data.ApplicationUsers.Add(appUser);

            var recipes = new List<Recipe>()
            {
                new Recipe()
                {
                    Id = "recipeId",
                    Name = "Musaka",
                    ServingsSize = 8,
                    UserId = "AppUserId",
                    Ingredients = new List<Ingredient>(),
                    CaloriesPerServing = 100,
                    ImageUrl = "imgUrl",
                    NutrtionId = 3,
                },
                 new Recipe()
                {
                    Id = "recipe2Id",
                    Name = "Musaka2",
                    ServingsSize = 8,
                    UserId = "AppUserId",
                    Ingredients = new List<Ingredient>(),
                    CaloriesPerServing = 80,
                    ImageUrl = "imgUrl2",
                    NutrtionId = 3,
                },
                     new Recipe()
                {
                    Id = "recipe3Id",
                    Name = "Pasta",
                    ServingsSize = 8,
                    UserId = "otherUser",
                    Ingredients = new List<Ingredient>(),
                    CaloriesPerServing = 80,
                    ImageUrl = "imgUrl3",
                    NutrtionId = 3,
                },
            };

            var ingredients = new List<Ingredient>()
            {
                new Ingredient()
                {
                    FoodId = this.TestFood.Id,
                    Food = this.TestFood,
                    Amount = 1,
                    Id = 1
                }
            };

            this.data.Ingredients.AddRange(ingredients);
            recipes[0].Ingredients.AddRange(ingredients);


            this.TestRecipe = recipes[0];
            this.data.Recipes.AddRange(recipes);

            this.data.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
        {
            this.data.Dispose();
        }
    }
}
