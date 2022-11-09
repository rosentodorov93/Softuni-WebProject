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

      
    }
}
