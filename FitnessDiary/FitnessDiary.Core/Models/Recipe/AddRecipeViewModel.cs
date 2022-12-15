using FitnessDiary.Core.Models.Food;
using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Recepie
{
    public class AddRecipeViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; } = null!;
        [Range(1, 100)]
        public int ServingsSize { get; set; }
        public string UserId { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public IEnumerable<FoodServiceModel> Foods { get; set; } = new List<FoodServiceModel>();

    }
}
