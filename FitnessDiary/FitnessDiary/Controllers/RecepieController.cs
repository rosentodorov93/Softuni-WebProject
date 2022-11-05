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

            await recepieService.AddAsync(model);

            return RedirectToAction($"AddIngredient/{model.Id}", "Recepie");
        }

        public async Task <IActionResult> AddIngredient(int Id)
        {
            var model = new AddIngredientViewModel()
            {
                RecepieId = Id,
                Foods = await foodService.GetAllAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> AddToCollection(AddIngredientViewModel model)
        {

            var recipe = await recepieService.AddIngredientAsync(model.Ingredient, model.RecepieId);

            return RedirectToAction("Details", "Recepie", recipe);
        }
        public IActionResult Details(DetailsViewModel model)
        {

            return View(model);
        }
    }
}
