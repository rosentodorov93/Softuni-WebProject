using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository repo;

        public ArticleService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Creates new article
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddAsync(AddViewModel model)
        {
            var article = new Article()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Author = model.Author,
                Date = DateTime.UtcNow.Date,
                CategoryId = model.CategoryId,
                Content = model.Content,
            };

            await repo.AddAsync<Article>(article);
            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Delete article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            var article = repo.All<Article>().Where(a => a.IsActive).FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                article.IsActive = false;
                await repo.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Edit article
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditAsync(ArticleDetailsViewModel model)
        {
            var article = repo.All<Article>().Where(a => a.IsActive).FirstOrDefault(a => a.Id == model.Id);

            if (article != null)
            {
                article.Author = model.Author;
                article.Date = model.Date;
                article.Title = model.Title;
                article.Content = model.Content;
                article.ImageUrl = model.ImageUrl;
                article.CategoryId = model.CategoryId;

                await repo.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Check if article exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string id)
        {
            return await repo.AllReadonly<Article>().Where(a => a.IsActive).AnyAsync(a => a.Id == id);
        }
        /// <summary>
        /// Returns all articles filtered by category
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ListingViewModel>> GetAllAsync(string? filter)
        {
            var articles =  repo.AllReadonly<Article>()
                .Where(a => a.IsActive)
                .Include(a => a.Category)
                .AsQueryable();

            if (filter != null)
            {
                articles = articles.Where(a => a.Category.Name == filter);
            }

            return await articles.Select(a => new ListingViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Category = a.Category.Name,
                Author = a.Author,
                Date = a.Date
            }).ToListAsync();
        }
        /// <summary>
        /// Returns article by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleDetailsViewModel> GetByIdAsync(string id)
        {
            var article = await repo.All<Article>()
                .Where(a => a.IsActive)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ArticleDetailsViewModel()
            {
                Id = article.Id,
                Author = article.Author,
                ImageUrl = article.ImageUrl,
                CategoryId = article.Category.Id,
                Content = article.Content,
                Date = article.Date,
                Title = article.Title
            };
        }
        /// <summary>
        /// Returns all article categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories =  await repo.AllReadonly<ArticleCategory>().ToListAsync();

            return categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            });
        }
        /// <summary>
        /// Returns category name from categor id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(string categoryId)
        {

            return repo.AllReadonly<ArticleCategory>().FirstOrDefault(a => a.Id == categoryId)?.Name ?? "";
        }
        /// <summary>
        /// Returns latest 3 articles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ListingViewModel>> GetLatestAsync()
        {
            var latestArticles = await repo.AllReadonly<Article>()
                .Where(a => a.IsActive)
                .Include(a => a.Category)
                .OrderByDescending(a => a.Date)
                .Take(3)
                .ToListAsync();

            return latestArticles.Select(a => new ListingViewModel()
            {
                Id = a.Id,
                Author = a.Author,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Category = a.Category.Name,
                Date = a.Date
            });
        }
    }
}
