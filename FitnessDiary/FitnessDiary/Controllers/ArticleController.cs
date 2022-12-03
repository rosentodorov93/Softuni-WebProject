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
        
        public async Task<IActionResult> Details(string Id)
        {
            var article = await articleService.GetByIdAsync(Id);

            return View(article);
        }
        
    }
}
