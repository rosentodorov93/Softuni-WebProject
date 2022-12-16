using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService accountService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMemoryCache cache;
        private ILogger logger;
        public UserController(IAccountService _accountService,
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            ILogger<UserController> _logger,
            IMemoryCache _cache)
        {
            accountService = _accountService;
            userManager = _userManager;
            roleManager = _roleManager;
            logger = _logger;
            cache = _cache;
        }

        public async Task<IActionResult> All()
        {
            var users = this.cache.Get<IEnumerable<AllUsersViewModel>>(UsersCacheKey);

            if (users == null)
            {
                users = await accountService.GetAllUsersAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                cache.Set(UsersCacheKey, users, cacheOptions);
            }

            return View(users);
        }

        public IActionResult Create()
        {
            var model = new CreateUserViewModel();

            model.Roles = roleManager.Roles.Select(r => r.Name).Where(n => n != "User").ToArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = roleManager.Roles.Select(r => r.Name).Where(n => n != "User").ToArray();
                return View(model);
            }

            await accountService.CreateAdministrationUser(model);
            cache.Remove(UsersCacheKey);
            logger.LogInformation($"Administration user {model.FirstName} {model.LastName} created!");

            return RedirectToAction(nameof(All));
        }
    }
}
