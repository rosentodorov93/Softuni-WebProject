using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        public async Task<IActionResult> All()
        {
            var articles = await articleService.GetAllAsync();

            return View(articles);
        }
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await articleService.AddAsync(model);

            return RedirectToAction("All");
        }
        public async Task<IActionResult> Details(string Id)
        {
            var article = await articleService.GetByIdAsync(Id);

            return View(article);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var article = await articleService.GetByIdAsync(Id);

            return View(article);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleDetailsViewModel model)
        {
            await articleService.EditAsync(model);

            return RedirectToAction("Details", "Article", new {Id = model.Id});
        }
    }
}
