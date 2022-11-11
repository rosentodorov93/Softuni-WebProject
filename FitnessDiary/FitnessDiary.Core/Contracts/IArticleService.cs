using FitnessDiary.Core.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IArticleService
    {
        Task AddAsync(AddViewModel model);
        Task<List<ListingViewModel>> GetAllAsync();
        Task<ArticleDetailsViewModel> GetByIdAsync(string id);
        Task EditAsync(ArticleDetailsViewModel model);
    }
}
