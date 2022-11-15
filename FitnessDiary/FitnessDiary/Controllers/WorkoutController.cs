using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService workoutService;

        public WorkoutController(IWorkoutService _workoutService)
        {
            workoutService = _workoutService;
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateWorkoutViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateWorkoutViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await workoutService.CreateAsync(model);

            return Json((await workoutService.CreateAsync(model)))
        }
        public async Task<IActionResult> Mine()
        {
            var workouts = await workoutService.GetMineAsync();

            return View(workouts);
        }
    }
}
