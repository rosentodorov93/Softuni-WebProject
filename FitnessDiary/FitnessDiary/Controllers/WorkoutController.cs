using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Core.Models.Workout;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService workoutService;

        public WorkoutController(IWorkoutService _workoutService)
        {
            workoutService = _workoutService;
        }

        public async Task<IActionResult> CreateTamplate()
        {
            var model = new CreateWorkoutViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTamplate([FromBody]CreateWorkoutViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Json(await workoutService.CreateTamplateAsync(model, userId));
        }
        public async Task<IActionResult> MineTamlates()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workouts = await workoutService.GetMineTamplatesAsync(userId);

            return View(workouts);
        }
        public async Task<IActionResult> EditTamplate(string Id)
        {
            var workoutTamplate = await workoutService.GetTamplateById(Id);

            return View(workoutTamplate);
        }
        [HttpPost]
        public async Task<IActionResult> EditTamplate(EditTamplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await workoutService.EditTamplateAsync(model);

            return RedirectToAction("MineTamlates");
        }
        [HttpPost]
        public async Task<IActionResult> AddExerciseToTamplate([FromBody] AddExerciseModel model)
        {
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
            await workoutService.RemoveExerciseAsync(model.ExerciseId, model.TamplateId);

            return Json("success");
        }
        public async Task<IActionResult> AddToDiary(string Id)
        {
            var tamplate = await workoutService.GetTamplateForDiaryByIdAsync(Id);

            return View(tamplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddToDiary(AddToDiaryViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddToDiary", "Workout", new { Id = model.Id });
            }

            await workoutService.AddToDiaryAsync(model, userId);

            return RedirectToAction("Index", "Diary");
        }
    }
}
