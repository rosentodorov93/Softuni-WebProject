namespace FitnessDiary.Core.Models.Recepie
{
    public class EditViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ServingsSize { get; set; }
        public string?  ImageUrl { get; set; }
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
    }
}
