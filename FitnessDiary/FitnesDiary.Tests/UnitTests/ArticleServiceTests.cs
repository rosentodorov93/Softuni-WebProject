using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Article;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;

namespace FitnesDiary.Tests.UnitTests
{
    public class ArticleServiceTests : UnitTestsBase
    {
        private IArticleService articleService;
        private IRepository repo;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.repo = new Repository(this.data);
            this.articleService = new ArticleService(repo);
        }

        [Test]
        public async Task AddArticle_ShouldAddCorrectly()
        {
            var countBeforeAdd = repo.AllReadonly<Article>().Where(a => a.IsActive).Count();
            var model = new AddViewModel()
            {
                Author = "Rosen",
                Title = "Add article",
                ImageUrl = "img",
                Content = "content",
                Date = DateTime.Now,
                CategoryId = "categoryId2"
            };

            await articleService.AddAsync(model);
            var countAfterAdd = repo.AllReadonly<Article>().Where(a => a.IsActive).Count();

            Assert.That(countAfterAdd, Is.EqualTo(countBeforeAdd + 1));
            Assert.IsTrue(repo.AllReadonly<Article>().Where(a => a.IsActive).Any(a => a.Title == model.Title));
            Assert.IsTrue(repo.AllReadonly<Article>().Where(a => a.IsActive).Any(a => a.Author == model.Author));
            Assert.IsTrue(repo.AllReadonly<Article>().Where(a => a.IsActive).Any(a => a.Content == model.Content));
        }
        [Test]
        public async Task EditArticle_ShouldEditCorrectly()
        {
            
            var model = new ArticleDetailsViewModel()
            {
                Author = "Edit",
                Title = "Edit",
                ImageUrl = "imgEdit",
                Content = "contentEdit",
                Date = DateTime.Now,
                CategoryId = "categoryId",
                Id = this.TestArticle.Id
            };

            await articleService.EditAsync(model);



            Assert.That(this.TestArticle.Title, Is.EqualTo(model.Title));
            Assert.That(this.TestArticle.Author, Is.EqualTo(model.Author));
            Assert.That(this.TestArticle.Content, Is.EqualTo(model.Content));
            Assert.That(this.TestArticle.ImageUrl, Is.EqualTo(model.ImageUrl));
        }
        [Test]
        public async Task EditArticle_ShouldNotEditInvalidArticle()
        {

            var model = new ArticleDetailsViewModel()
            {
                Author = "test",
                Title = "test",
                ImageUrl = "imgtest",
                Content = "contentTest",
                Date = DateTime.Now,
                CategoryId = "categoryId",
                Id = "invalid"
            };

            await articleService.EditAsync(model);



            Assert.That(this.TestArticle.Title, Is.Not.EqualTo(model.Title));
            Assert.That(this.TestArticle.Author, Is.Not.EqualTo(model.Author));
            Assert.That(this.TestArticle.Content, Is.Not.EqualTo(model.Content));
            Assert.That(this.TestArticle.ImageUrl, Is.Not.EqualTo(model.ImageUrl));
        }
        [Test]
        public async Task GetAll_ShouldReturnAllArticlesWithoutFilter()
        {
            var articlesCount = data.Articles.Where(a => a.IsActive).Count();
            

            var result = await articleService.GetAllAsync(null);



            Assert.That(result.Count, Is.EqualTo(articlesCount));
            
        }
        [Test]
        public async Task GetAll_ShouldReturnCorrectResultWithFilter()
        {
            
            var expectedCount = data.Articles.Where(a => a.IsActive).Where(a => a.Category.Name == "nutrition").Count();


            var result = await articleService.GetAllAsync("nutrition");



            Assert.That(result.Count, Is.EqualTo(expectedCount));

        }
        [Test]
        public async Task GetById_ShouldReturnCorrectArticle()
        {

            var result = await articleService.GetByIdAsync(this.TestArticle.Id);



            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo(this.TestArticle.Title));
            Assert.That(result.Author, Is.EqualTo(this.TestArticle.Author));
            Assert.That(result.Content, Is.EqualTo(this.TestArticle.Content));

        }
        [Test]
        public async Task ExistsById_ShouldReturnTrueWithValidArticle()
        {

            var result = await articleService.ExistsById(this.TestArticle.Id);

            Assert.IsTrue(result);

        }
        [Test]
        public async Task ExistsById_ShouldReturnFalseWithInvalidArticle()
        {

            var result = await articleService.ExistsById("invalid");

            Assert.IsFalse(result);

        }
        [Test]
        public async Task GetCategories_ShouldReturnAllCategoriesNamesCorrectly()
        {
            
            var result = await articleService.GetCategoriesAsync();

            Assert.That(2, Is.EqualTo(result.Count()));

        }
        [Test]
        public void GetCategoryName_ShouldReturnNameWithValidId()
        {

            var result = articleService.GetCategoryName("categoryId");

            Assert.That("testCategory", Is.EqualTo(result));

        }
        [Test]
        public void GetCategoryName_ShouldReturnEmtyStringWithInvalidId()
        {

            var result = articleService.GetCategoryName("invalid");

            Assert.That(string.Empty, Is.EqualTo(result));

        }
        [Test]
        public async Task DeleteSync_ShouldRemoveArticleCorrectly()
        {

            var countBeforeDelete = data.Articles.Where(a => a.IsActive).Count();
            var latestId = data.Articles.Last().Id;

            await articleService.DeleteAsync(latestId);
            var countAfterDelete = data.Articles.Where(a => a.IsActive).Count();

            Assert.That(countAfterDelete, Is.EqualTo(countBeforeDelete - 1));

        }
        [Test]
        public async Task DeleteSync_ShouldNotRemoveArticleWithInvalidId()
        {

            var countBeforeDelete = data.Articles.Where(a => a.IsActive).Count();

            await articleService.DeleteAsync("Invalid");
            var countAfterDelete = data.Articles.Where(a => a.IsActive).Count();

            Assert.That(countAfterDelete, Is.EqualTo(countBeforeDelete));

        }
        [Test]
        public async Task GetLatest_ReturnsCorrectNumber()
        {

            data.Articles.AddRange(
                new Article
                {
                    Id = "first",
                    Author = "",
                    Title = "",
                    CategoryId = "categoryId",
                    Content = "",
                    Date = DateTime.Now,
                    ImageUrl = "",
                    IsActive = true,
                },
                new Article
                {
                    Id = "second",
                    Author = "",
                    Title = "",
                    CategoryId = "categoryId",
                    Content = "",
                    Date = DateTime.Now,
                    ImageUrl = "",
                    IsActive = true,
                },
                new Article
                {
                    Id = "third",
                    Author = "",
                    Title = "",
                    CategoryId = "categoryId",
                    Content = "",
                    Date = DateTime.Now,
                    ImageUrl = "",
                    IsActive = true,
                });
            data.SaveChanges();

            var result = await articleService.GetLatestAsync();


            Assert.That(3, Is.EqualTo(result.Count()));

        }
    }
}
