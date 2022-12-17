using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        public ArticleController(IArticleService _articleService, ILogger<ArticleController> _logger, IMemoryCache _cache)
        {
            articleService = _articleService;
            logger = _logger;
            cache = _cache;
        }

        public async Task<IActionResult> All(AllArticlesQueryModel query)
        {
            var articles = await articleService.GetAllAsync(query.CategoryFilter);

            query.Articles = articles;
            query.Categories = cache.Get<IEnumerable<CategoryViewModel>>(ArticlesCategoriesCacheKey);

            if (query.Categories == null)
            {
                query.Categories = await articleService.GetCategoriesAsync();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                cache.Set(ArticlesCategoriesCacheKey, query.Categories, cacheOptions);
            }

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
