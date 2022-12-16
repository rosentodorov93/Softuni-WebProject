using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    [Authorize(Roles = "User")]
    public class DiaryController : Controller
    {
        private readonly IDiaryService diaryService;
        private readonly IAccountService accountService;
        private ILogger logger;

        public DiaryController(IDiaryService _service,
            IAccountService _accountService,
            ILogger<DiaryController> _logger)
        {
            diaryService = _service;
            accountService = _accountService;
            logger = _logger;
        }

        public async Task<IActionResult> Index()
        {
            var applicationUserId = accountService.GetById(this.User.Id());
            if (await accountService.ExistsById(applicationUserId) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Ivalid user Id";
                logger.LogError("Invalid user");

                return RedirectToAction("Index", "Home", new { area = "" }); 
            }
            
            var diaryDay = await diaryService.LoadDiaryDay(applicationUserId);
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
            
            var userId = accountService.GetById(this.User.Id());

            if (await accountService.ExistsById(userId) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Ivalid user Id";
                logger.LogError("Invalid user");


                return RedirectToAction("Index", "Home", new { area = "" });
            }
            try
            {
                var result = await diaryService.AddRecipeServingAsync(userId, model.Id, model.Amount, model.Category);
                logger.LogInformation(result);

                return Json(result);
            }
            catch (ArgumentException ae)
            {

                TempData[MessageConstant.ErrorMessage] = ae.Message;
                logger.LogError(ae, ae.Message);

                return RedirectToAction(nameof(Index));
            }
            
        }
        public async Task<IActionResult> RemoveServing(int Id)
        {
            var userId = accountService.GetById(this.User.Id());

            if (await accountService.ExistsById(userId) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Ivalid user Id";
                logger.LogError("Invalid user");


                return RedirectToAction("Index", "Home", new { area = "" });
            }

            try
            {
                await diaryService.RemoveServingAsync(userId, Id);

                return RedirectToAction("Index", "Diary");
            }
            catch (ArgumentException ae)
            {
                TempData[MessageConstant.ErrorMessage] = ae.Message;
                logger.LogError(ae, ae.Message);

                return RedirectToAction(nameof(Index));
            }
        }
       
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddFoodServing([FromBody] ServingServiceModel model)
        {
            var userId = accountService.GetById(this.User.Id());

            if (await accountService.ExistsById(userId) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Ivalid user Id";
                logger.LogError("Invalid user");


                return RedirectToAction("Index", "Home", new { area = "" });
            }

            try
            {
                var result = await diaryService.AddFoodServingAsync(userId, model.Id, model.Amount, model.Category);
                logger.LogInformation(result);

                return Json(result);
            }
            catch (ArgumentException ae)
            {

                TempData[MessageConstant.ErrorMessage] = ae.Message;
                logger.LogError(ae, ae.Message);

                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}
