using FitnessDiary.Core.Models.Account;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using Microsoft.AspNetCore.Identity;

namespace FitnessDiary.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<ActivityLevel>> GetActivityLevels();
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
        Task<NutritionServiceModel> GetUserTargetNutritionAsync(string userId);
        Task<bool> CreateApplicationUser(IdentityUser user,
            int age,
            string fullName,
            int gender,
            int height,
            double weight,
            int activityLevel,
            int fitnessGoal);
        string? GetById(string id);
        Task CreateAdministrationUser(CreateUserViewModel model);
        Task<bool> ExistsById(string? userId);
    }
}
