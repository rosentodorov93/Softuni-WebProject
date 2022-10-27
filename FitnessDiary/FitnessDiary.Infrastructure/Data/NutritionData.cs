using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class NutritionData
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public double Calories { get; set; }

        [Required]
        public double Carbohydrates { get; set; }

        [Required]
        public double Proteins { get; set; }

        [Required]
        public double Fats { get; set; }

        public IList<Food> Foods { get; set; } = new List<Food>();
    }
}
