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
        public Article TestArticle { get; private set; } = null!;

        [OneTimeTearDown]
        public void TearDownBase()
        {
            this.data.Dispose();
        }
        private void SeedDatabase()
        {
            SeedFood();
            SeedUser();
            SeedRecipes();
            SeedWorkoutTamplates();
            SeedArticle();

            this.data.SaveChanges();
        }

        private void SeedFood()
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
                new NutritionData()
               {
                    Id = 4,
                    Calories = 0,
                    Carbohydrates = 0,
                    Proteins = 0,
                    Fats = 0
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
        }
        private void SeedUser()
        {
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

            var diaryDay = new DiaryDay()
            {
                DateTime = DateTime.Now.Date,
                Id = 1,
                NutritionId = 4,
            };

            this.data.DiaryDays.Add(diaryDay);
            appUser.Diary.Add(diaryDay);

            this.AppUser = appUser;

            this.data.ApplicationUsers.Add(appUser);
        }
        private void SeedRecipes()
        {
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
        }
        private void SeedWorkoutTamplates()
        {
            var exercises = new List<ExerciseTamplate>()
            {
                new ExerciseTamplate()
                {
                     Id = "firstExercise",
                     Name = "Bench Press",
                     BodyPart = FitnessDiary.Infrastructure.Data.Enums.BodyPartType.Chest,
                     SetCount = 3
                },
                new ExerciseTamplate()
                {
                     Id = "secondExercise",
                     Name = "Shoulder Press",
                     BodyPart = FitnessDiary.Infrastructure.Data.Enums.BodyPartType.Sholders,
                     SetCount = 3
                },
                new ExerciseTamplate()
                {
                     Id = "thirdExercise",
                     Name = "Skull Crushers",
                     BodyPart = FitnessDiary.Infrastructure.Data.Enums.BodyPartType.Tricep,
                     SetCount = 3
                }
            };

            this.data.ExerciseTamplates.AddRange(exercises);

            var tamplate = new WorkoutTamplate()
            {
                Id = "tamplateId",
                Name = "Upper body Workout",
                Description = "Chest, Shoulders, Triceps",
                User = this.AppUser,
                Exercises = exercises
            };

            this.TestTamplate = tamplate;
            this.data.WorkoutTamplates.Add(tamplate);
        }
        private void SeedArticle()
        {
            var articleCategories = new List<ArticleCategory>()
            {
                new ArticleCategory()
                {
                    Id = "categoryId",
                    Name = "testCategory"
                },
                new ArticleCategory()
                {
                    Id = "categoryId2",
                    Name = "nutrition"
                }
            };

            this.data.ArticleCategories.AddRange(articleCategories);

            var article = new Article()
            {
                Title = "TestArticle",
                Author = "Admin",
                Date = DateTime.Now,
                IsActive = true,
                CategoryId = "categoryId",
                Content = "Test Article Content",
                Id = "articleId",
                ImageUrl = "img",
            };

            this.TestArticle = article;

            this.data.Articles.Add(article);
        }
    }
}
