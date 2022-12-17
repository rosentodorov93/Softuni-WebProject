using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.RecipeDataConstants;

namespace FitnessDiary.Core.Models.Recepie
{
    public class CreateRecipeModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;
        [Range(MinServingSize,MaxServingSize)]
        public int ServingsSize { get; set; }
        public string UserId { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = null!;
    }
}
