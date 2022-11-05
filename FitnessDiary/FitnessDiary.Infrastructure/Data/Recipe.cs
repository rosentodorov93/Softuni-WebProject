using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public List<Food> Foods { get; set; } = new List<Food>();
        public int ServingsSize { get; set; }
        public MeassureUnitType Unit { get; set; }

        [Required]
        public int NutrtionId { get; set; }

        [ForeignKey(nameof(NutrtionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public double CaloriesPerServing { get; set; }
        public bool isFinished { get; set; } = false;

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
