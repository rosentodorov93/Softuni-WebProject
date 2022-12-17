using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.FoodDataConstants;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.NutritionDataConstants;

namespace FitnessDiary.Core.Models.Food
{
    public class FoodViewModel
    {

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(MaxTypeLength, MinimumLength = MinTypeLength)]
        public string Type { get; set; } = null!;

        [Required]
        [Range(typeof(double), MinNutritionMetric, MaxNutritionMetric)]
        public double Calories { get; set; } 

        [Required]
        public int MeassureUnit { get; set; }

        [Required]
        [Range(typeof(double), MinNutritionMetric, MaxNutritionMetric)]
        public double Carbohydtrates { get; set; }

        [Required]
        [Range(typeof(double), MinNutritionMetric, MaxNutritionMetric)]
        public double Proteins { get; set; }

        [Required]
        [Range(typeof(double), MinNutritionMetric, MaxNutritionMetric)]
        public double Fats { get; set; }
    }
}
