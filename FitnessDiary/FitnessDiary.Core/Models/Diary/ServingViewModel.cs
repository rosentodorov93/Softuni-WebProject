namespace FitnessDiary.Core.Models.Diary
{
    public class ServingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public double Amount { get; set; }
        public NutritionServiceModel Nutrition { get; set; } = null!;
    }
}