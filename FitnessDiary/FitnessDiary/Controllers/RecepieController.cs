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

        public RecepieController(IFoodService _foodService, IRecipeService _recipeService)
        {
            foodService = _foodService;
            recepieService = _recipeService;
        }

        public IActionResult Add()
        {
            var model = new CreateViewModel();
            ViewBag.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultId = await recepieService.AddAsync(model);

            return RedirectToAction($"Details", "Recepie", new { id = resultId });
        }

        public async Task<IActionResult> AddIngredient(string id)
        {
            var model = new AddIngredientViewModel()
            {
                RecepieId = id,
                //Foods = await foodService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientViewModel model)
        {

            var recipe = await recepieService.AddIngredientAsync(model.Ingredient, model.RecepieId);

            return RedirectToAction("Details", "Recepie", new { id = recipe.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveIngredient(string id)
        {
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
            await recepieService.RemoveIngredient(model.Recipeid, model.IngredientToRemove);

            return RedirectToAction("Details", "Recepie", new { id = model.Recipeid });
        }
        public async Task<IActionResult> Details(string id)
        {
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
                Unit = recipe.Unit,
                Ingredients = recipe.Ingredients
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            await recepieService.EditAsync(model);

            return RedirectToAction("Details", "Recepie", new { id = model.Id });
        }
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var recipes = await recepieService.GetAllById(userId);

            return View(recipes);
        }
    }
}
