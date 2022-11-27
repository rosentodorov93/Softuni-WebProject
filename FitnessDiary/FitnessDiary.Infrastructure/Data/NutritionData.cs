using System.ComponentModel.DataAnnotations;

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
    }
}
