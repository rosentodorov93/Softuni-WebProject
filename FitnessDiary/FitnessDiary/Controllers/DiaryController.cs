using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
    [Authorize(Roles = "User")]
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
            var applicationUserId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var diaryDay = await diaryService.GetByIdAsync(applicationUserId);
            var requiredNutrition = await accountService.GetUserTargetNutritionAsync(applicationUserId);

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
                },
                Workout = diaryDay.Workout
            };

            return View(model);

        }
      
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddRecipeServing([FromBody] ServingServiceModel model)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await diaryService.AddRecipeServingAsync(userId, model.Id, model.Amount, model.Category);

            return Json("success");
        }
        public async Task<IActionResult> RemoveServing(int Id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await diaryService.RemoveServingAsync(userId, Id);

            return RedirectToAction("Index", "Diary");
        }
        public IActionResult Statistics()
        {

            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddFoodServing([FromBody] ServingServiceModel model)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await diaryService.AddFoodServingAsync(userId, model.Id, model.Amount, model.Category);

            return Json("success");
        }
    }
}
