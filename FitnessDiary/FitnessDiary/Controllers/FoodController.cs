using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Controllers
{
  
    public class FoodController : Controller
    {
        private readonly IFoodService service;
        private readonly IAccountService accountService;

        public FoodController(IFoodService _service, IAccountService _accountService)
        {
            service = _service;
            accountService = _accountService;
        }
        [Authorize(Roles = "User,Admin,Moderator")]
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

        [Authorize(Roles = "User,Admin,Moderator")]
        public async Task<IActionResult> Add()
        {
            var model = new FoodViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FoodViewModel model)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddFood(model, userId);

            return RedirectToAction("All", "Food");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Mine([FromQuery] MinePageViewModel query)
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

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

        [Authorize(Roles = "User,Admin,Moderator")]
        public async Task<IActionResult> Edit(string Id)
        {
            if ((await service.ExistsByIdAsync(Id) == false))
            {
                return RedirectToAction("Mine");
            }
            var food = await service.GetByIdAsync(Id);

            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, FoodViewModel model)
        {
            if ((await service.ExistsByIdAsync(Id)) == false)
            {
                ModelState.AddModelError("", "Food does not exist");

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.EditAsync(Id, model);

            return RedirectToAction("Mine");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]string Id)
        {
            if ((await service.ExistsByIdAsync(Id)))
            {
                await service.DeleteAsync(Id);
            }

            return Json("success");
        }
    }
}
