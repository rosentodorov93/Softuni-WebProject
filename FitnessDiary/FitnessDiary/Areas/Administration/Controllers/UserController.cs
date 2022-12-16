using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService accountService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private ILogger logger;
        public UserController(IAccountService _accountService,
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            ILogger<UserController> _logger)
        {
            accountService = _accountService;
            userManager = _userManager;
            roleManager = _roleManager;
            logger = _logger;
        }

        public async Task<IActionResult> All()
        {
            var users = await accountService.GetAllUsersAsync();

            return View(users);
        }

        public IActionResult Create()
        {
            var model = new CreateUserViewModel();

            model.Roles = roleManager.Roles.Select(r => r.Name).ToArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = roleManager.Roles.Select(r => r.Name).ToArray();
                return View(model);
            }

            await accountService.CreateAdministrationUser(model);
            logger.LogInformation($"Administration user {model.FirstName} {model.LastName} created!");

            return RedirectToAction(nameof(All));
        }
    }
}
