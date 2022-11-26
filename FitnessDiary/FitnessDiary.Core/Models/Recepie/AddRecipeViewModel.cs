using FitnessDiary.Core.Models.Food;

namespace FitnessDiary.Core.Models.Recepie
{
    public class AddRecipeViewModel
    {
        public string Name { get; set; } = null!;
        public int ServingsSize { get; set; }
        public string UserId { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public IEnumerable<FoodServiceModel> Foods { get; set; } = new List<FoodServiceModel>();

    }
}
