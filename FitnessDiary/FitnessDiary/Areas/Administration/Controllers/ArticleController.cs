using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Areas.Administration.Controllers
{
    public class ArticleController : AdminController
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        public IActionResult Add()
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

            return RedirectToAction("All", "Article", new { Area = "" });
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

            return RedirectToAction("All", "Article", new { Area = "" });
        }
    }
}
