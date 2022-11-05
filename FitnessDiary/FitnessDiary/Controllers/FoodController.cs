using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Mine([FromQuery] MinePageViewModel query)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var foodsResult = await service.GetAllById(userId, query.Type, query.SearchTerm, query.Sorting, query.CurrentPage);

            var foodTypes = await service.getAllTypesAsync();

            query.Types = foodTypes;
            query.TotalFoods = foodsResult.TotalFoods;
            query.Foods = foodsResult.Foods;

            return View(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(string foodId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await service.AddToCollectionAsync(userId, foodId);

            return RedirectToAction("Mine", "Food");
        }

    }
}
