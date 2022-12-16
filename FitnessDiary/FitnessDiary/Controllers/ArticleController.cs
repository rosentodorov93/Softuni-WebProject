using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ILogger logger;

        public ArticleController(IArticleService _articleService, ILogger<ArticleController> _logger)
        {
            articleService = _articleService;
            logger = _logger;
        }

        public async Task<IActionResult> All(AllArticlesQueryModel query)
        {
            var articles = await articleService.GetAllAsync(query.CategoryFilter);

            query.Articles = articles;
            query.Categories = await articleService.GetCategoriesAsync();

            return View(query);
        }
        
        public async Task<IActionResult> Details(string Id)
        {
            if ((await articleService.ExistsById(Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid article Id";
                logger.LogError("Invalid article Id");

                RedirectToAction(nameof(All));
            }

            var article = await articleService.GetByIdAsync(Id);

            return View(article);
        }
        
    }
}
