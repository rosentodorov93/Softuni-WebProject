using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Food
{
    public class FoodViewModel
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Type { get; set; } = null!;

        [Required]
        public double Calories { get; set; } 

        [Required]
        public int MeassureUnit { get; set; }

        [Required]
        public double Carbohydtrates { get; set; }

        [Required]
        public double Proteins { get; set; }

        [Required]
        public double Fats { get; set; }
    }
}
