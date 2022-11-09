namespace FitnessDiary.Core.Models.Diary
{
    public class ServingViewModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public NutritionServiceModel Nutrition { get; set; }
    }
}