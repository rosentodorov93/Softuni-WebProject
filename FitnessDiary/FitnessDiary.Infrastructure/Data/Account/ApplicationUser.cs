using FitnessDiary.Infrastructure.Data.Enums;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
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
        public ActivityLevel ActivityLevel { get; set; } = null!;

        [Required]
        public FitnessGoalType FitnessGoal { get; set; }

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData TargetNutrients { get; set; } = null!;
        public int CarbsPercent { get; set; } = 50;
        public int ProteinPercent { get; set; } = 20;
        public int FatsPercent { get; set; } = 30;
        public IList<DiaryDay> Diary { get; set; } = new List<DiaryDay>();
        public IList<Food> Foods { get; set; } = new List<Food>();
        public IList<Recipe> Recipes { get; set; } = new List<Recipe>();
        public IList<Article> Articles { get; set; } = new List<Article>();
        public IList<WorkoutTamplate> WorkoutTamplates { get; set; } = new List<WorkoutTamplate>();

    }
}
