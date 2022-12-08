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

        public async Task AddAsync(AddViewModel model)
        {
            var article = new Article()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Author = model.Author,
                Date = DateTime.UtcNow.Date,
                Category = model.Category,
                Content = model.Content,
            };

            await repo.AddAsync<Article>(article);
            await repo.SaveChangesAsync();
        }

        public async Task EditAsync(ArticleDetailsViewModel model)
        {
            var article = repo.All<Article>().FirstOrDefault(a => a.Id == model.Id);

            if (article != null)
            {
                article.Author = model.Author;
                article.Date = model.Date;
                article.Title = model.Title;
                article.Content = model.Content;
                article.Category = model.Category;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<List<ListingViewModel>> GetAllAsync()
        {
            var articles = await repo.All<Article>().ToListAsync();

            return articles.Select(a => new ListingViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Category = a.Category,
                Author = a.Author,
                Date = a.Date
            }).ToList();
        }

        public async Task<ArticleDetailsViewModel> GetByIdAsync(string id)
        {
            var article = repo.All<Article>().FirstOrDefault(a => a.Id == id);

            return new ArticleDetailsViewModel()
            {
                Id = article.Id,
                Author = article.Author,
                ImageUrl = article.ImageUrl,
                Category = article.Category,
                Content = article.Content,
                Date = article.Date,
                Title = article.Title
            };
        }

        public async Task<IEnumerable<ListingViewModel>> GetLatestAsync()
        {
            var latestArticles = await repo.AllReadonly<Article>()
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.Date)
                .Take(3)
                .ToListAsync();

            return latestArticles.Select(a => new ListingViewModel()
            {
                Id = a.Id,
                Author = a.Author,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Category = a.Category,
                Date = a.Date
            });
        }
    }
}
