using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ActivityLevel ActivityLevel { get; set; }= null!;

        [Required]
        public string FitnessGoal { get; set; } = null!;

        [Required]
        public double DailyTargetCalories { get; set; }
    }
}
