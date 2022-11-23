using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Models;
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
        public async Task<IActionResult> All([FromQuery] AllFoodsQueryModel query)
        {
            var result = await service.GetAllAsync(
                null,
                query.Type, 
                query.SearchTerm, 
                query.Sorting, 
                query.CurrentPage, 
                AllFoodsQueryModel.FoodsPerPage);

            query.TotalFoods = result.TotalFoodsCount;
            query.Types = await service.getAllTypesAsync();
            query.Foods = result.Foods;

            return View(query);
        }

        public async Task<IActionResult> AddToMine()
        {
            var model = new FoodViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMine(FoodViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddFood(model, userId);

            return RedirectToAction("All", "Food");
        }
        public async Task<IActionResult> Mine([FromQuery] MinePageViewModel query)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await service.GetAllAsync(
                userId,
                query.Type,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllFoodsQueryModel.FoodsPerPage);

            query.TotalFoods = result.TotalFoodsCount;
            query.Types = await service.getAllTypesAsync();
            query.Foods = result.Foods;

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
