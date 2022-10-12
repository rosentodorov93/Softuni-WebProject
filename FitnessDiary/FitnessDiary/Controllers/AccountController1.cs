using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class AccountController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
