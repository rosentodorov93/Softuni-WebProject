using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesDiary.Tests.UnitTests
{
    public class DiaryServiceTests : UnitTestsBase
    {
        private IDiaryService diaryService;
        private IRepository repo;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.repo = new Repository(this.data);
            this.diaryService = new DiaryService(repo);
        }

        [Test]
        public async Task LoadDiaryDay_ShouldCreateNewDayOnFirstCall()
        {
            var result = await diaryService.LoadDiaryDay(this.AppUser.Id);

            Assert.That(result.Date, Is.EqualTo(DateTime.Now.Date));
        }
        [Test]
        public async Task AddFoodServing_ShouldAddCorrectly()
        {
            var serving = new ServingServiceModel()
            {
                Id = this.TestFood.Id,
                Category = "Breakfast",
                Amount = 2,
            };
            var expectedCalories = this.TestFood.Nutrition.Calories * serving.Amount;

            await diaryService.AddFoodServingAsync(this.AppUser.Id, serving.Id, serving.Amount, serving.Category);
            var result = await diaryService.LoadDiaryDay(this.AppUser.Id);

            Assert.That(result.BreakfastServings.Count, Is.EqualTo(1));
            Assert.IsTrue(result.BreakfastServings.Any(s => s.Name == this.TestFood.Name));
            Assert.IsTrue(result.BreakfastServings.Any(s => s.Amount == serving.Amount));
            Assert.That(result.Nutrition.Calories, Is.EqualTo(expectedCalories));

        }
        [Test]
        public void AddFoodServing_ShouldThrowErrorWithInvalidUser()
        {
            var serving = new ServingServiceModel()
            {
                Id = this.TestFood.Id,
                Category = "Breakfast",
                Amount = 2,
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await diaryService.AddFoodServingAsync("invalidId", serving.Id, serving.Amount, serving.Category));
        }
        [Test]
        public void AddFoodServing_ShouldThrowErrorWithInvalidFood()
        {
            var serving = new ServingServiceModel()
            {
                Id = "invalid",
                Category = "Breakfast",
                Amount = 2,
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await diaryService.AddFoodServingAsync(this.AppUser.Id, serving.Id, serving.Amount, serving.Category));
        }
        [Test]
        public async Task RemoveServing_ShouldWorkCorrectly()
        {
            await diaryService.RemoveServingAsync(this.AppUser.Id, 1);
            await diaryService.RemoveServingAsync(this.AppUser.Id, 2);

            var result = await diaryService.LoadDiaryDay(this.AppUser.Id);

            Assert.That(result.BreakfastServings.Count, Is.EqualTo(0));
      
            Assert.That(result.Nutrition.Calories, Is.EqualTo(0));
        }
        [Test]
        public async Task AddRecipeServing_ShouldAddCorrectly()
        {
            var resultBeforeAdd = await diaryService.LoadDiaryDay(this.AppUser.Id);
            var caloriesBeforeAdd = resultBeforeAdd.Nutrition.Calories;
            var serving = new ServingServiceModel()
            {
                Id = this.TestRecipe.Id,
                Category = "Breakfast",
                Amount = 2,
            };
            var expectedCalories = this.TestRecipe.Nutrition.Calories * serving.Amount;

            await diaryService.AddRecipeServingAsync(this.AppUser.Id, serving.Id, serving.Amount, serving.Category);
            var result = await diaryService.LoadDiaryDay(this.AppUser.Id);

            Assert.That(result.BreakfastServings.Count, Is.EqualTo(2));
            Assert.IsTrue(result.BreakfastServings.Any(s => s.Name == this.TestRecipe.Name));
            Assert.IsTrue(result.BreakfastServings.Any(s => s.Amount == serving.Amount));
            Assert.That(result.Nutrition.Calories - caloriesBeforeAdd, Is.EqualTo(expectedCalories));

        }
        [Test]
        public void AddRecipeServing_ShouldThrowErrorWithInvalidUser()
        {
            var serving = new ServingServiceModel()
            {
                Id = this.TestRecipe.Id,
                Category = "Breakfast",
                Amount = 2,
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await diaryService.AddFoodServingAsync("invalidId", serving.Id, serving.Amount, serving.Category));
        }
        [Test]
        public void AddRecipeServing_ShouldThrowErrorWithInvalidFood()
        {
            var serving = new ServingServiceModel()
            {
                Id = "invalid",
                Category = "Breakfast",
                Amount = 2,
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await diaryService.AddFoodServingAsync(this.AppUser.Id, serving.Id, serving.Amount, serving.Category));
        }
    }
}
