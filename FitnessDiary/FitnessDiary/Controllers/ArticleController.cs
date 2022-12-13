using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Models;
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
                TempData[MessageConstant.ErrorMessage] = "Invalid Id";
                RedirectToAction(nameof(All));
            }
            var article = await articleService.GetByIdAsync(Id);

            return View(article);
        }
        
    }
}
