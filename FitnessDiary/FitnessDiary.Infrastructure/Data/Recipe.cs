using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.RecipeDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class Recipe
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MinNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        [Range(MinServingSize, MaxServingSize)]
        public int ServingsSize { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public int NutrtionId { get; set; }

        [ForeignKey(nameof(NutrtionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public double CaloriesPerServing { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
