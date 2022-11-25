using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data
{
    public class Recipe
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int ServingsSize { get; set; }

        [Required]
        public int NutrtionId { get; set; }

        [ForeignKey(nameof(NutrtionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public double CaloriesPerServing { get; set; }
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
