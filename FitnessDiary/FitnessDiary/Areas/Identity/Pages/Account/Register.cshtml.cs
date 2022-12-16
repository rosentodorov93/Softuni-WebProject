// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using FitnessDiary.Core.Contracts;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using static FitnessDiary.Areas.Administration.Constants.AdminConstants;

namespace FitnessDiary.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountService _service;
        private readonly IMemoryCache cache;


        private readonly ILogger<RegisterModel> _logger;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAccountService accountService,
            ILogger<RegisterModel> logger,
            IMemoryCache _cache)
        {
            _userManager = userManager;
            _service = accountService;

            _signInManager = signInManager;
            _logger = logger;
            cache = _cache;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public IEnumerable<ActivityLevel> ActivityLevels { get; set; } = new List<ActivityLevel>();


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(25, MinimumLength = 5)]
            public string Username { get; set; } = null!;
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [StringLength(80, MinimumLength = 5)]
            public string FullName { get; set; } = null!;

            [Required]
            [Range(1, 110)]
            public int Age { get; set; }

            [Required]
            public int Gender { get; set; }

            [Required]
            [Range(1, 250)]
            public int Height { get; set; }

            [Required]
            [Range(typeof(double), "0.0", "500.0")]
            public double Weight { get; set; }

            [Required]
            public int ActivityLevelId { get; set; }

            [Required]
            public int FitnessGoal { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            this.ActivityLevels = await _service.GetActivityLevels();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {                
                var user = new IdentityUser()
                {
                    Email = Input.Email,
                    UserName = Input.Username
                };
                
                await _userManager.CreateAsync(user, Input.Password);

                await _userManager.AddToRoleAsync(user, "User");

               
                var result = await _service.CreateApplicationUser(
                    user, 
                    Input.Age,
                    Input.FullName, 
                    Input.Gender, 
                    Input.Height, 
                    Input.Weight, 
                    Input.ActivityLevelId,
                    Input.FitnessGoal);

                if (result)
                {
                    _logger.LogInformation("User created a new account with password.");
                    cache.Remove(UsersCacheKey);

                    return RedirectToPage("/Account/Login", new { area = "Identity" });

                }
            }

            this.ActivityLevels = await _service.GetActivityLevels();
            return Page();
        }

    }
}
