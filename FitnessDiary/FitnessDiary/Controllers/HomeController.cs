using FitnessDiary.Core.Constants;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService _articleService)
        {
            _logger = logger;
            articleService = _articleService;
        }

        public async Task<IActionResult> Index()
        {
            var latestArticles = await articleService.GetLatestAsync();
            return View(latestArticles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}