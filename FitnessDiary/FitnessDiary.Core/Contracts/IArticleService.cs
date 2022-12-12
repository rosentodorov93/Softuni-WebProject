using FitnessDiary.Core.Models.Article;

namespace FitnessDiary.Core.Contracts
{
    public interface IArticleService
    {
        Task AddAsync(AddViewModel model);
        Task<List<ListingViewModel>> GetAllAsync(string? filter=null);
        Task<ArticleDetailsViewModel> GetByIdAsync(string id);
        Task EditAsync(ArticleDetailsViewModel model);
        Task<IEnumerable<ListingViewModel>> GetLatestAsync();
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        string GetCategoryName(string categoryId);
        Task<bool> ExistsById(string id);
        Task DeleteAsync(string id);
    }
}
