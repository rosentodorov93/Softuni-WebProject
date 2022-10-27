using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService service;

        public FoodController(IFoodService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> All()
        {
            var foods = await service.GetAllAsync();

            return View(foods);
        }

        public async Task<IActionResult> Add()
        {
            var model = new FoodViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FoodViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddFood(model);

            return RedirectToAction("All", "Food");
        }

    }
}
