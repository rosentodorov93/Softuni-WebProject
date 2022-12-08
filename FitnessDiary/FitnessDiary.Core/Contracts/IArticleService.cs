using FitnessDiary.Core.Models.Article;

namespace FitnessDiary.Core.Contracts
{
    public interface IArticleService
    {
        Task AddAsync(AddViewModel model);
        Task<List<ListingViewModel>> GetAllAsync();
        Task<ArticleDetailsViewModel> GetByIdAsync(string id);
        Task EditAsync(ArticleDetailsViewModel model);
        Task<IEnumerable<ListingViewModel>> GetLatestAsync();
    }
}
