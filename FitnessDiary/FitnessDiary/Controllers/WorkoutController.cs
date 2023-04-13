using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static FitnessDiary.Core.Constants.WorkoutConstants;
using static FitnessDiary.Core.Constants.UserConstants;


namespace FitnessDiary.Controllers
{
    [Authorize(Roles = UserRole)]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService workoutService;
        private readonly IAccountService accountService;
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        public WorkoutController(IWorkoutService _workoutService
            , IAccountService _accountService
            , ILogger<WorkoutController> _logger
            , IMemoryCache _cache)
        {
            workoutService = _workoutService;
            accountService = _accountService;
            logger = _logger;
            cache = _cache;
        }

        public  IActionResult CreateTamplate()
        {
            var model = new CreateWorkoutViewModel();
            return View(model);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateTamplate([FromBody]CreateWorkoutViewModel model)
        {
            var userId = accountService.GetById(this.User.Id());
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                logger.LogError("Invalid user Id");

                return RedirectToAction(nameof(MineTamplates));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await workoutService.CreateTamplateAsync(model, userId);
            cache.Remove(WorkoutConstants.MineTamplatesCacheKey);
            logger.LogInformation(result);

            return Json(result);
        }
        public async Task<IActionResult> MineTamplates()
        {
            var userId = accountService.GetById(this.User.Id());
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidUserId);

                return RedirectToAction("Index", "Home");
            }
            var tamplates = cache.Get<IEnumerable<ListingTamplateViewModel>>(WorkoutConstants.MineTamplatesCacheKey);

            if (tamplates == null)
            {
                tamplates =  await workoutService.GetMineTamplatesAsync(userId);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                cache.Set(WorkoutConstants.MineTamplatesCacheKey, tamplates, cacheOptions);
            }

            return View(tamplates);
        }
        public async Task<IActionResult> EditTamplate(string Id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamplates));
            }

            var workoutTamplate = await workoutService.GetTamplateById(Id);

            return View(workoutTamplate);
        }
        [HttpPost]
        public async Task<IActionResult> EditTamplate(EditTamplateViewModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidTamplateId);

                return RedirectToAction(nameof(MineTamplates));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await workoutService.EditTamplateAsync(model);
            cache.Remove(WorkoutConstants.MineTamplatesCacheKey);

            return RedirectToAction(nameof(MineTamplates));
        }
        public async Task<IActionResult> EditWorkout(string Id)
        {
            if ((await workoutService.WorkoutExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidTamplateId);

                return RedirectToAction(nameof(MineTamplates));
            }

            var model = await workoutService.GetWorkoutByIdAsync(Id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditWorkout(WorkoutViewModel model)
        {
            if ((await workoutService.WorkoutExistsByIdAsync(model.Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamplates));
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Diary");
            }

            await workoutService.EditWorkoutAsync(model);

            return RedirectToAction("Index", "Diary");
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddExerciseToTamplate([FromBody] AddExerciseModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.WorkoutId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidTamplateId);

                return RedirectToAction(nameof(MineTamplates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MineTamplates));
            }

            try
            {
                var result = await workoutService.AddExerciseToTamplateAsync(model);
                cache.Remove(WorkoutConstants.MineTamplatesCacheKey);
                logger.LogInformation(result);

                return Json(result);
            }
            catch (ArgumentException ae)
            {

                TempData[MessageConstant.ErrorMessage] = ae.Message;
                logger.LogError(ae, ae.Message);

                return BadRequest(ae.Message);
            }
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RemoveExerciseFromTamplate([FromBody] RemoveExerciseModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.TamplateId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidTamplateId);

                return RedirectToAction(nameof(MineTamplates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MineTamplates));
            }

            var result = await workoutService.RemoveExerciseAsync(model.ExerciseId, model.TamplateId);
            TempData[MessageConstant.SuccessMessage] = result;
            cache.Remove(WorkoutConstants.MineTamplatesCacheKey);
            logger.LogInformation(result);

            return Ok(result);
        }
        public async Task<IActionResult> AddToDiary(string Id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamplates));
            }

            var tamplate = await workoutService.GetTamplateForDiaryByIdAsync(Id);

            return View(tamplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddToDiary(AddToDiaryViewModel model)
        {
            var userId = accountService.GetById(this.User.Id());
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidUserId;
                logger.LogError(InvalidUserId);

                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MineTamplates));
            }

            try
            {
                await workoutService.AddToDiaryAsync(model, userId);

                return RedirectToAction("Index", "Diary");
            }
            catch (InvalidOperationException e)
            {

                TempData[MessageConstant.ErrorMessage] = e.Message;

                return RedirectToAction("Index", "Diary");
            }
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteTamplate([FromBody] string id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidTamplateId;
                logger.LogError(InvalidTamplateId);

                return RedirectToAction(nameof(MineTamplates));
            }

            var result = await workoutService.DeleteAsync(id);
            cache.Remove(WorkoutConstants.MineTamplatesCacheKey);
            logger.LogInformation(result);

            return Json(result);
        }

    }
}
