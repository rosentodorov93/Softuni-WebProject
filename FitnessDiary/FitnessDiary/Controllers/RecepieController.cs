using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
    [Authorize(Roles = "User")]
    public class RecepieController : Controller
    {
        private readonly IFoodService foodService;
        private readonly IRecipeService recepieService;
        private readonly IAccountService accountService;

        public RecepieController(IFoodService _foodService, IRecipeService _recipeService, IAccountService _accountService)
        {
            foodService = _foodService;
            recepieService = _recipeService;
            accountService = _accountService;
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddRecipeViewModel();
            model.Foods = await foodService.LoadIngedientsAsync();
            model.UserId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier)) ?? String.Empty;

            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add([FromBody] CreateRecipeModel model)
        {
            if ((await accountService.ExistsById(model.UserId)) == false)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await recepieService.AddAsync(model);

            return Json("success");
        }

        public async Task<IActionResult> AddIngredient(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction(nameof(Mine));
            }
            var model = new AddIngredientViewModel()
            {
                RecepieId = id,
                Foods = await foodService.LoadIngedientsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientInputModel model)
        {
            if ((await recepieService.ExistsByIdAsync(model.RecepieId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction(nameof(Mine));
            }
            if ((await foodService.ExistsByIdAsync(model.Ingredient.FoodId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid ingredient Id";
                return RedirectToAction(nameof(Mine));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }

            var recipe = await recepieService.AddIngredientAsync(model.Ingredient, model.RecepieId);
            return RedirectToAction("Details", "Recepie", new { id = recipe.Id });

        }

        [HttpGet]
        public async Task<IActionResult> RemoveIngredient(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction("Mine");
            }
            var ingredients = await recepieService.GetIngredientsAsync(id);

            var model = new RemoveIngredientViewModel()
            {
                Recipeid = id,
                Ingredients = ingredients
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveIngredient(RemoveIngredientInputModel model)
        {
            if ((await recepieService.ExistsByIdAsync(model.Recipeid)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction(nameof(Mine));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }
            try
            {
                await recepieService.RemoveIngredient(model.Recipeid, model.IngredientToRemove);

                return RedirectToAction("Details", "Recepie", new { id = model.Recipeid });
            }
            catch (ArgumentException e)
            {
                TempData[MessageConstant.ErrorMessage] = e.Message;
                return RedirectToAction("Mine");
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction("Mine");
            }
            var recipe = await recepieService.GetByIdAsync(id);
            return View(recipe);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                return RedirectToAction("Mine");
            }

            var recipe = await recepieService.GetByIdAsync(id);
            var model = new EditViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                Ingredients = recipe.Ingredients,
                ImageUrl = recipe.ImageUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if ((await recepieService.ExistsByIdAsync(model.Id)) == false)
            {
                return RedirectToAction("Mine");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }
            try
            {
                await recepieService.EditAsync(model);

                return RedirectToAction("Details", "Recepie", new { id = model.Id });
            }
            catch (Exception)
            {

                TempData[MessageConstant.ErrorMessage] = "Unexpected Error"; ;
                return View(model.Id);
            }
        }
        public async Task<IActionResult> Mine()
        {
            var userId = accountService.GetById(this.User.Id());
            if ((await accountService.ExistsById(userId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var recipes = await recepieService.GetAllById(userId);

            return View(recipes);
        }
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                return RedirectToAction("Mine");
            }

            await recepieService.DeleteAsync(id);
            return Json("success");
        }
    }
}
