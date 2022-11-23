using FitnessDiary.Core.Models.Enums;
using FitnessDiary.Core.Models.Food;

namespace FitnessDiary.Models
{
    public class AllFoodsQueryModel
    {
        public const int FoodsPerPage = 2;
        public IEnumerable<string> Types { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string SearchTerm { get; set; } = null!;
        public FoodSorting Sorting { get; set; }
        public int TotalFoods { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PreviousPage => CurrentPage - 1;
        public double MaxPage => Math.Ceiling((double)TotalFoods / FoodsPerPage);

        public IEnumerable<FoodServiceModel> Foods { get; set; } = null!;
    }
}
