using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Models.Recepie;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers
{
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
            model.UserId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRecipeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await  recepieService.AddAsync(model);

            return Json("success");
        }

        public async Task<IActionResult> AddIngredient(string id)
        {
            var model = new AddIngredientViewModel()
            {
                RecepieId = id,
                Foods = await foodService.LoadIngedientsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }

            try
            {
                var recipe = await recepieService.AddIngredientAsync(model.Ingredient, model.RecepieId);
                return RedirectToAction("Details", "Recepie", new { id = recipe.Id });
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError("", ae.Message);
                return View(model.RecepieId);
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model.RecepieId);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveIngredient(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
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
        public async Task<IActionResult> RemoveIngredient(RemoveIngredientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Mine");
            }
            try
            {
                await recepieService.RemoveIngredient(model.Recipeid, model.IngredientToRemove);

                return RedirectToAction("Details", "Recepie", new { id = model.Recipeid });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return RedirectToAction("Mine");
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            if ((await recepieService.ExistsByIdAsync(id)) == false)
            {
                return RedirectToAction("Mine");
            }
            var recipe = await recepieService.GetByIdAsync(id);
            return View(recipe);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var recipe = await recepieService.GetByIdAsync(id);
            var model = new EditViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                ServingsSize = recipe.ServingsSize,
                Ingredients = recipe.Ingredients
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

                ModelState.AddModelError("", "Unexpected Error");
                return View(model.Id);
            }
        }
        public async Task<IActionResult> Mine()
        {
            var userId = accountService.GetById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var recipes = await recepieService.GetAllById(userId);

            return View(recipes);
        }
        public async Task<IActionResult> Delete([FromBody]string id)
        {
            if ((await recepieService.ExistsByIdAsync(id))== false)
            {
                return RedirectToAction("Mine");
            }

            await recepieService.DeleteAsync(id);
            return Json("success");
        }
    }
}
