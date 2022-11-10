using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
    public class DiaryController : Controller
    {
        private readonly IDiaryService diaryService;
        private readonly IAccountService accountService;

        public DiaryController(IDiaryService _service, IAccountService _accountService)
        {
            diaryService = _service;
            accountService = _accountService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var diaryDay = await diaryService.GetByIdAsync(userId);
            var requiredNutrition = await accountService.GetUserTargetNutritionAsync(userId);

            var model = new IdexViewQueryModel()
            {
                CurrentDayDate = diaryDay.Date,
                Breakfast = diaryDay.BreakfastServings,
                Lunch = diaryDay.LunchServings,
                Dinner = diaryDay.DinnerServings,
                Snack = diaryDay.SnackServings,
                NutritionStatistics = new NutritionStatisticsViewModel()
                {
                    CurrentNutrition = diaryDay.Nutrition,
                    RequiredNutrition = requiredNutrition
                }
            };

            return View(model);

        }
        public async Task<IActionResult> AddFromDatabase()
        {
            var model = new AddServingViewModel()
            {
                Foods = await diaryService.GetFoodsFromDbAsync(),
                Serving = new ServingServiceModel()
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddFromDatabase(AddServingViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await diaryService.AddFromDatabaseAsync(userId, model.Serving.Id, model.Serving.Amount, model.Serving.Category);

            return RedirectToAction("Index", "Diary");
        }
        public async Task<IActionResult> AddFromMine()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new AddServingViewModel()
            {
                Foods = await diaryService.GetMineFoodsFromDbAsync(userId),
                Serving = new ServingServiceModel()
            };

            return View(model);
        }
        public async Task<IActionResult> AddRecipe()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new AddServingViewModel()
            {
                Foods = await diaryService.GetRecipesFromDbAsync(userId),
                Serving = new ServingServiceModel()
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddRecipe(AddServingViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await diaryService.AddRecipeAsync(userId, model.Serving.Id, model.Serving.Amount, model.Serving.Category);

            return RedirectToAction("Index", "Diary");
        }
        public async Task<IActionResult> RemoveServing(int Id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await diaryService.RemoveServingAsync(userId, Id);

            return RedirectToAction("Index", "Diary");
        }

    }
}
