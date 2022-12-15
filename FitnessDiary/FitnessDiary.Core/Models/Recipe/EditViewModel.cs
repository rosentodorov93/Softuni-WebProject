using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Recepie
{
    public class EditViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; } = null!;

        [Range(1,100)]
        public int ServingsSize { get; set; }
        public string?  ImageUrl { get; set; }
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
    }
}
