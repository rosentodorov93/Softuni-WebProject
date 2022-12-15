namespace FitnessDiary.Core.Models.Recepie
{
    public class AddIngredientInputModel
    {
        public string RecepieId { get; set; } = null!;
        public IngredientViewModel Ingredient { get; set; } = null!;
    }
}
