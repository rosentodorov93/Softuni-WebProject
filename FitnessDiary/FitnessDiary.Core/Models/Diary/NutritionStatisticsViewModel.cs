namespace FitnessDiary.Core.Models.Diary
{
    public class NutritionStatisticsViewModel
    {
        public NutritionServiceModel CurrentNutrition { get; set; } = null!;
        public NutritionServiceModel RequiredNutrition { get; set; } = null!;

    }
}