using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Account;
using FitnessDiary.Infrastructure.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService _service,
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            service = _service;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                ActivityLevels = service.GetActivityLevels(),
                FitnessGoals = service.GetFitnessGoals()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser()
            {
                UserName = model.Username,
                FullName = model.FullName,
                Email = model.Email,
                Age = model.Age,
                Height = model.Height,
                Weight = model.Weight,
                ActivityLevelId = model.ActivityLevelId,
                FitnessGoalId = model.FitnessGoalId

            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
