using FitnessDiary.Core.Models.Food;

namespace FitnessDiary.Core.Models.Recepie
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<IngredientViewModel> Foods { get; set; } = new List<IngredientViewModel>();
        public int ServingsSize { get; set; }
        public int Unit { get; set; }
        public double TotalCalories { get; set; }
        public double CaloriesPerPortion { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public bool isFinished { get; set; }
        public string UserId { get; set; }

    }
}
