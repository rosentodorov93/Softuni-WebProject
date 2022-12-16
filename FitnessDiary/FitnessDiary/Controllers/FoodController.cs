using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Extensions;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Controllers
{

    [Authorize(Roles = "Admin, Moderator, User")]
    public class FoodController : Controller
    {
        private readonly IFoodService service;
        private readonly IAccountService accountService;
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        public FoodController(IFoodService _service, IAccountService _accountService, ILogger<FoodController> _logger, IMemoryCache _cache)
        {
            service = _service;
            accountService = _accountService;
            logger = _logger;
            cache = _cache;
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
            query.Foods = cache.Get<IEnumerable<FoodServiceModel>>(FoodConstants.AllFoodsCacheKey);

            if (query.Foods == null)
            {
                query.Foods = result.Foods;
            }


            return View(query);
        }

        public IActionResult Add()
        {
            var model = new FoodViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FoodViewModel model)
        {
            var userId = accountService.GetById(this.User.Id());

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddFood(model, userId);
            if (userId == null)
            {
                logger.LogInformation($"New food {model.Name} added to food Database");
            }

            return RedirectToAction("All", "Food");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Mine([FromQuery] MinePageViewModel query)
        {
            var userId = accountService.GetById(this.User.Id());

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

        public async Task<IActionResult> Edit(string Id)
        {
            if ((await service.ExistsByIdAsync(Id) == false))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var foodHasUser = await service.FoodHasAppUser(Id);

            if (this.User.IsInRole("User") && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = "You can't edit public food database";
                logger.LogError($"User {this.User.Identity.Name} don't have access to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole("Admin") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't edit private foods";
                logger.LogError($"Admin {this.User.Identity.Name} don't have access to to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole("Moderator") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't edit private foods";
                logger.LogError($"Moderator {this.User.Identity.Name} don't have access to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
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

            var foodHasUser = await service.FoodHasAppUser(Id);

            if (this.User.IsInRole("User") && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = "You can't edit public food database";
                logger.LogError($"User {this.User?.Identity?.Name} don't have access to food with id {Id}");
                return View(model);
            }

            if (this.User.IsInRole("Admin") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't edit private foods";
                logger.LogError($"Admin {this.User?.Identity?.Name} don't have access to food with id {Id}");
                return View(model);
            }

            if (this.User.IsInRole("Moderator") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't edit private foods";
                logger.LogError($"Moderator {this.User?.Identity?.Name} don't have access to food with id {Id}");
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
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete([FromBody] string Id)
        {
            if ((await service.ExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid food Id";
                logger.LogError($"Invalid Id");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var foodHasUser = await service.FoodHasAppUser(Id);

            if (this.User.IsInRole("User") && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = "You can't delete public food database";
                logger.LogError($"User {this.User.Identity.Name} don't have access to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole("Admin") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't delete private foods";
                logger.LogError($"Admin {this.User.Identity.Name} don't have access to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole("Moderator") && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = "Admin can't delete private foods";
                logger.LogError($"Moderator {this.User.Identity.Name} don't have access to food with id {Id}");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return Json(await service.DeleteAsync(Id));
        }
    }
}
