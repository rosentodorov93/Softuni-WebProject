using FitnessDiary.Core.Contracts;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly IArticleService articleService;

        public HomeController(ILogger<HomeController> _logger, IArticleService _articleService)
        {
            logger = _logger;
            articleService = _articleService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Diary");
            }
            var latestArticles = await articleService.GetLatestAsync();
            return View(latestArticles);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            logger.LogError(feature.Error, "TraceIdentifier: {0}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}