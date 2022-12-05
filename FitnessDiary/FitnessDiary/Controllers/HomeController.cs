using FitnessDiary.Core.Constants;
using FitnessDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //if (this.User.IsInRole(AdminRoleName))
            //{
            //    return RedirectToAction("Index", "Home", new { area = AdministrationAreaName });
            //}
            var user = this.User;
            return View();
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