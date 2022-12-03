using FitnessDiary.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FitnessDiary.Areas.Administration.Controllers
{
    public class UserController : AdminController
    {
        private readonly IAccountService accountService;
        public UserController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            var users = await accountService.GetAllUsersAsync();

            return View(users);
        }
    }
}
