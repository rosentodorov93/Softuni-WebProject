namespace FitnessDiary.Core.Models.Recepie
{
    public class RemoveIngredientViewModel : RemoveIngredientInputModel
    {
        public IEnumerable<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
       
    }
}
