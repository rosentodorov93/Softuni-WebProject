using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.AdministrationUserDataConstants;
namespace FitnessDiary.Core.Models.Account
{
    public class CreateAdministrationUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        public string RoleName { get; set; } = null!;

        [Required]
        [StringLength(MaxFirstNameLength, MinimumLength = MinFirstNameLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(MaxLastNameLength, MinimumLength = MinLastNameLength)]
        public string LastName { get; set; } = null!;
        public string[]? Roles { get; set; }
    }
}
