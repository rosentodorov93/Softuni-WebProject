using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data.Account
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [StringLength(80)]
        public string FullName { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public int  Height { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [StringLength(7)]
        public Gender Gender { get; set; }

        [Required]
        public int ActivityLevelId { get; set; }

        [Required]
        [ForeignKey(nameof(ActivityLevelId))]
        public ActivityLevel ActivityLevel { get; set; }= null!;

        [Required]
        public int FitnessGoalId { get; set; }

        [ForeignKey(nameof(FitnessGoalId))]
        public FitnessGoal FitnessGoal { get; set; } = null!;

        [Required]
        public int DiaryId { get; set; }

        [ForeignKey(nameof(DiaryId))]
        public Diary Diary { get; set; } = null!;
        public IList<Food> Foods { get; set; } = new List<Food>();
        public IList<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
