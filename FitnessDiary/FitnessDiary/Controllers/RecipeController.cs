﻿using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using static FitnessDiary.Core.Constants.UserConstants;
using static FitnessDiary.Core.Constants.RecipeConstants;

namespace FitnessDiary.Controllers
{
    [Authorize(Roles = UserRole)]
    public class RecipeController : Controller
    {
        private readonly IFoodService foodService;
        private readonly IRecipeService recepieService;
        private readonly IAccountService accountService;
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        public RecipeController(IFoodService _foodService,
            IRecipeService _recipeService,
            IAccountService _accountService,
            ILogger<RecipeController> _logger,
            IMemoryCache _cache)
        {
            foodService = _foodService;
            recepieService = _recipeService;
            accountService = _accountService;
            logger = _logger;
            cache = _cache;
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
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await recepieService.AddAsync(model);
            TempData[MessageConstant.SuccessMessage] = result;
            cache.Remove(RecipeConstants.MineRecipesCacheKey);
            logger.LogInformation(result);

            return Json(result);
        }

        public async Task<IActionResult> AddIngredient(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

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
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

                return RedirectToAction(nameof(Mine));
            }
            if ((await foodService.ExistsByIdAsync(model.Ingredient.FoodId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidIngredientId;
                logger.LogError(InvalidIngredientId);

                return RedirectToAction(nameof(Mine));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }

            var recipe = await recepieService.AddIngredientAsync(model.Ingredient, model.RecepieId);
            cache.Remove(RecipeConstants.MineRecipesCacheKey);

            return RedirectToAction("Details", "Recipe", new { id = recipe.Id });

        }

        [HttpGet]
        public async Task<IActionResult> RemoveIngredient(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

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
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

                return RedirectToAction(nameof(Mine));
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }
            try
            {
                await recepieService.RemoveIngredient(model.Recipeid, model.IngredientToRemove);
                cache.Remove(RecipeConstants.MineRecipesCacheKey);

                return RedirectToAction("Details", "Recipe", new { id = model.Recipeid });
            }
            catch (ArgumentException e)
            {
                TempData[MessageConstant.ErrorMessage] = e.Message;
                logger.LogError(e, e.Message);
                return RedirectToAction("Mine");
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

                return RedirectToAction("Mine");
            }
            var recipe = await recepieService.GetDetailsByIdAsync(id);
            return View(recipe);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

                return RedirectToAction("Mine");
            }

            var recipe = await recepieService.GetByIdAsync(id);

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if ((await recepieService.ExistsByIdAsync(model.Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid recipe Id";
                logger.LogError("Invalid recipe Id");

                return RedirectToAction("Mine");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }

            await recepieService.EditAsync(model);
            cache.Remove(RecipeConstants.MineRecipesCacheKey);
            logger.LogInformation(string.Format(RecipeEdited,model.Name));

            return RedirectToAction("Details", "Recipe", new { id = model.Id });


        }
        public async Task<IActionResult> Mine()
        {
            var userId = accountService.GetById(this.User.Id());
            if ((await accountService.ExistsById(userId)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidUserId;
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var recipes = cache.Get<IEnumerable<RecipeListingViewModel>>(RecipeConstants.MineRecipesCacheKey);

            if (recipes == null)
            {
                recipes = await recepieService.GetAllByUserId(userId);
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                cache.Set(RecipeConstants.MineRecipesCacheKey, recipes, cacheOptions);
            }

            return View(recipes);
        }
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = InvalidRecipeId;
                logger.LogError(InvalidRecipeId);

                return RedirectToAction(nameof(Mine));
            }

            var result = await recepieService.DeleteAsync(id);

            TempData[MessageConstant.ErrorMessage] = result;
            cache.Remove(RecipeConstants.MineRecipesCacheKey);
            logger.LogInformation(result);

            return Json(result);
        }
    }
}
