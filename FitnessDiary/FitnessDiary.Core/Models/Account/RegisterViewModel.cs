using FitnessDiary.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string FullName { get; set; } = null!;

        [Required]
        [Range(1, 110)]
        public int Age { get; set; }

        [Required]
        [Range(1,250)]
        public int Height { get; set; }

        [Required]
        [Range(typeof(double), "0.0", "500.0")]
        public double Weight { get; set; }

        [Required]
        public int ActivityLevelId { get; set; }

        public IEnumerable<ActivityLevel> ActivityLevels { get; set; } = new List<ActivityLevel>();

        [Required]
        public int FitnessGoalId { get; set; }

        public IEnumerable<FitnessGoal> FitnessGoals { get; set; } = new List<FitnessGoal>();
    }
}
