using FitnessDiary.Core.Models.Account;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;

namespace FitnessDiary.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<ActivityLevel>> GetActivityLevels();
        Task<NutritionServiceModel> CalculateTargetNutrientsAsync(ApplicationUser user);
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
        Task<NutritionServiceModel> GetUserTargetNutritionAsync(string userId);
        Task AddApplicationUser(ApplicationUser applicationUser);
        string? GetById(string id);
        Task CreateUserAsync(CreateUserViewModel model);
    }
}
