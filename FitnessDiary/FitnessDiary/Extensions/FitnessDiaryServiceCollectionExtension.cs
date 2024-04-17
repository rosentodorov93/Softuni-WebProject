using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Extensions
{
    public static class FitnessDiaryServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IRepository, Repository>()
            .AddScoped<IFoodService, FoodService>()
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IDiaryService, DiaryService>()
            .AddScoped<IArticleService, ArticleService>()
            .AddScoped<IWorkoutService, WorkoutService>()
            .AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }
        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();


            return services;
        }
    }
}
