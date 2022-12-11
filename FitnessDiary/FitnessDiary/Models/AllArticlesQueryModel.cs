using FitnessDiary.Core.Models.Article;

namespace FitnessDiary.Models
{
    public class AllArticlesQueryModel
    {
        public string? CategoryFilter { get; set; } = null;
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<ListingViewModel> Articles { get; set; } = new List<ListingViewModel>();
    }
}
