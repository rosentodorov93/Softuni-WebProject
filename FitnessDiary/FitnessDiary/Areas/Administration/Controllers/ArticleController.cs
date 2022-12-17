using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Areas.Administration.Controllers
{
    public class ArticleController : AdministrationController
    {
        private readonly IArticleService articleService;
        private readonly ILogger logger;


        public ArticleController(IArticleService _articleService, ILogger<ArticleController> _logger, IMemoryCache _cache)
        {
            articleService = _articleService;
            logger = _logger;
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel();
            model.Categories = await articleService.GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await articleService.GetCategoriesAsync();
                return View(model);
            }

            await articleService.AddAsync(model);
            logger.LogInformation($"Article {model.Title} created!");

            return RedirectToAction("All", "Article", new { Area = "" });
        }
        public async Task<IActionResult> Edit(string Id)
        {
            if (await articleService.ExistsById(Id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid Id";
                RedirectToAction("All", "Article", new { area = "" });
            }
            var article = await articleService.GetByIdAsync(Id);
            article.Categories = await articleService.GetCategoriesAsync();

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleDetailsViewModel model)
        {
            if (await articleService.ExistsById(model.Id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid user Id";
                RedirectToAction("All", "Article", new { area = "" });
            }

            await articleService.EditAsync(model);

            return RedirectToAction("All", "Article", new { Area = "" });
        }

        public async Task<IActionResult> Delete(string Id)
        {
            if ((await articleService.ExistsById(Id)) == false)
            {
                return RedirectToAction("All", "Article", new { area = "" });
            }


            await articleService.DeleteAsync(Id);

            logger.LogInformation($"Deleted article with id: {Id}");



            return RedirectToAction("All", "Article", new { area = "" });
        }
    }
}
