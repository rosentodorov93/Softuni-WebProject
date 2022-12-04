namespace FitnessDiary.Core.Models.Account
{
    public class CreateUserViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string[]? Roles { get; set; }
    }
}
