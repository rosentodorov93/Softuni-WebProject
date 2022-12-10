using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
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

        public WorkoutController(IWorkoutService _workoutService, IAccountService _accountService)
        {
            workoutService = _workoutService;
            accountService = _accountService;
        }

        public  IActionResult CreateTamplate()
        {
            var model = new CreateWorkoutViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTamplate([FromBody]CreateWorkoutViewModel model)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Json(await workoutService.CreateTamplateAsync(model, userId));
        }
        public async Task<IActionResult> MineTamlates()
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
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
                return RedirectToAction(nameof(MineTamlates));
            }

            var model = await workoutService.GetWorkoutByIdAsync(Id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditWorkout(WorkoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Diary");
            }

            await workoutService.EditWorkoutAsync(model);

            return RedirectToAction("Index", "Diary");
        }
        [HttpPost]
        public async Task<IActionResult> AddExerciseToTamplate([FromBody] AddExerciseModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.WorkoutId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MineTamlates");
            }

            await workoutService.AddExerciseToTamplateAsync(model);

            return Json("success");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveExerciseFromTamplate([FromBody] RemoveExerciseModel model)
        {
            if ((await workoutService.TamplateExistsByIdAsync(model.TamplateId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                return RedirectToAction(nameof(MineTamlates));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MineTamlates");
            }
            await workoutService.RemoveExerciseAsync(model.ExerciseId, model.TamplateId);

            return Json("success");
        }
        public async Task<IActionResult> AddToDiary(string Id)
        {
            if ((await workoutService.TamplateExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid workout tamplate Id";
                return RedirectToAction(nameof(MineTamlates));
            }
            var tamplate = await workoutService.GetTamplateForDiaryByIdAsync(Id);

            return View(tamplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddToDiary(AddToDiaryViewModel model)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MineTamlates));
            }

            await workoutService.AddToDiaryAsync(model, userId);

            return RedirectToAction("Index", "Diary");
        }
    }
}
