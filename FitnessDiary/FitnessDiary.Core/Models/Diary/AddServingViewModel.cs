namespace FitnessDiary.Core.Models.Diary
{
    public class AddServingViewModel
    {
        public List<FoodDiaryServiceModel> Foods { get; set; } = null!;
        public ServingServiceModel Serving { get; set; } = null!;
    }
}
