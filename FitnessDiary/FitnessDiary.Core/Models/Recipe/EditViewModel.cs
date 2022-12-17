using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.RecipeDataConstants;

namespace FitnessDiary.Core.Models.Recepie
{
    public class EditViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Range(MinServingSize,MaxServingSize)]
        public int ServingsSize { get; set; }
        public string?  ImageUrl { get; set; }
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
    }
}
