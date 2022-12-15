using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
    [Authorize(Roles = "User")]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService workoutService;
        private readonly IAccountService accountService;
        private readonly ILogger logger;

        public WorkoutController(IWorkoutService _workoutService
            , IAccountService _accountService
            , ILogger<WorkoutController> _logger)
        {
            workoutService = _workoutService;
            accountService = _accountService;
            logger = _logger;
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

                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await workoutService.CreateTamplateAsync(model, userId);
            logger.LogInformation(result);

            return Json(result);
        }
        public async Task<IActionResult> MineTamlates()
        {
            var userId = accountService.GetById(this.User.Id());
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                logger.LogError("Invalid user Id");

                return RedirectToAction("Index", "Home");
            }

            var workouts = await workoutService.GetMineTamplatesAsync(userId);

            return View(workouts);
        }
        public async Task<IActionResult> EditTamplate(string Id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
            }

            var workoutTamplate = await workoutService.GetTamplateById(Id);

            return View(workoutTamplate);
        }
        [HttpPost]
        public async Task<IActionResult> EditTamplate(EditTamplateViewModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await workoutService.EditTamplateAsync(model);

            return RedirectToAction("MineTamlates");
        }
        public async Task<IActionResult> EditWorkout(string Id)
        {
            if ((await workoutService.WorkoutExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
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

                return RedirectToAction(nameof(MineTamlates));
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
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MineTamlates");
            }

            var result = await workoutService.AddExerciseToTamplateAsync(model);
            logger.LogInformation(result);

            return Json(result);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RemoveExerciseFromTamplate([FromBody] RemoveExerciseModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.TamplateId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MineTamlates");
            }

            var result = await workoutService.RemoveExerciseAsync(model.ExerciseId, model.TamplateId);
            logger.LogInformation(result);

            return Json(result);
        }
        public async Task<IActionResult> AddToDiary(string Id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
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
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                logger.LogError("Invalid user Id");

                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MineTamlates));
            }

            await workoutService.AddToDiaryAsync(model, userId);

            return RedirectToAction("Index", "Diary");
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteTamplate([FromBody] string id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                logger.LogError("Invalid workout tamplate Id");

                return RedirectToAction(nameof(MineTamlates));
            }

            var result = await workoutService.DeleteAsync(id);
            logger.LogInformation(result);

            return Json(result);
        }

    }
}
