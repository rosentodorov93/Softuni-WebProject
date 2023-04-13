using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Extensions;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;
using static FitnessDiary.Core.Constants.UserConstants;
using static FitnessDiary.Core.Constants.FoodConstants;

namespace FitnessDiary.Controllers
{

    [Authorize(Roles = "Admin, Moderator, User")]
    public class FoodController : Controller
    {
        private readonly IFoodService service;
        private readonly IAccountService accountService;
        private readonly ILogger logger;


        public FoodController(IFoodService _service, IAccountService _accountService, ILogger<FoodController> _logger)
        {
            service = _service;
            accountService = _accountService;
            logger = _logger;
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

            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                logger.LogInformation($"New food {model.Name} added to food Database");
            }

            if (User.IsInRole("User"))
            {
                return RedirectToAction("Mine", "Food");
            }

            return RedirectToAction("All", "Food");
        }

        [Authorize(Roles = UserRole)]
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

            if (this.User.IsInRole(UserRole) && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = UserPublicFoodError;
                logger.LogError(String.Format(UserPublicFoodLogError, this.User.Identity.Name, Id));
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole(AdminRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = AdminPrivateFoodError;
                logger.LogError(String.Format(AdminPrivateFoodLogError, this.User.Identity.Name, Id));
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole(ModeratorRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = ModeratorPrivateFoodError;
                logger.LogError(String.Format(ModeratorPrivateFoodLogError, this.User.Identity.Name, Id));
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
                ModelState.AddModelError("", FoodDoesNotExist);

                return View(model);
            }

            var foodHasUser = await service.FoodHasAppUser(Id);

            if (this.User.IsInRole(UserRole) && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = UserPublicFoodError;
                logger.LogError(UserPublicFoodLogError, this.User.Identity.Name, Id);
                return View(model);
            }

            if (this.User.IsInRole(AdminRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = AdminPrivateFoodError;
                logger.LogError(String.Format(AdminPrivateFoodLogError, this.User.Identity.Name, Id));
                return View(model);
            }

            if (this.User.IsInRole(ModeratorRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = ModeratorPrivateFoodError;
                logger.LogError(String.Format(ModeratorPrivateFoodLogError, this.User.Identity.Name, Id));
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.EditAsync(Id, model);

            if (User.IsInRole(AdminRoleName)|| User.IsInRole(ModeratorRoleName))
            {
                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(Mine));
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete([FromBody] string Id)
        {
            if ((await service.ExistsByIdAsync(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidFoodId;
                logger.LogError(InvalidFoodId);
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var foodHasUser = await service.FoodHasAppUser(Id);

            if (this.User.IsInRole(UserRole) && foodHasUser == false)
            {
                TempData[MessageConstant.ErrorMessage] = UserPublicFoodError;
                logger.LogError(String.Format(UserPublicFoodLogError, this.User.Identity.Name, Id));
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole(AdminRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = AdminPrivateFoodError;
                logger.LogError(String.Format(AdminPrivateFoodLogError, this.User.Identity.Name, Id));
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (this.User.IsInRole(ModeratorRoleName) && foodHasUser)
            {
                TempData[MessageConstant.ErrorMessage] = ModeratorPrivateFoodError;
                logger.LogError(String.Format(ModeratorPrivateFoodLogError, this.User.Identity.Name, Id));
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return Json(await service.DeleteAsync(Id));
        }
    }
}
