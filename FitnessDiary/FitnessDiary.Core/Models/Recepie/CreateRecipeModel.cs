using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Recepie
{
    public class CreateRecipeModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; } = null!;
        [Range(1,100)]
        public int ServingsSize { get; set; }
        public string UserId { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = null!;
    }
}
