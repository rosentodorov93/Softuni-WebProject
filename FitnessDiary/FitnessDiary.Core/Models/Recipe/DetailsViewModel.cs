namespace FitnessDiary.Core.Models.Recepie
{
    public class DetailsViewModel: RecipeListingViewModel
    {
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
    }
}
