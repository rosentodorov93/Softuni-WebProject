using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Account;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;
        private readonly UserManager<IdentityUser> userManager;

        public AccountService(IRepository _repo, UserManager<IdentityUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task AddApplicationUser(ApplicationUser applicationUser)
        {
            await repo.AddAsync(applicationUser);
            await repo.SaveChangesAsync();
        }

        public async Task<NutritionServiceModel> CalculateTargetNutrientsAsync(ApplicationUser user)
        {
            double targetCalories = 0;

            if (user.Gender == Gender.Male)
            {
                targetCalories = 66.5 + (13.75 * user.Weight) + (5.003 * user.Height) - (6.75 * user.Age);
            }
            else
            {
                targetCalories = 655.1 + (9.563 * user.Weight) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            var activityLevelValue = await repo.AllReadonly<ActivityLevel>().FirstOrDefaultAsync(a => a.Id == user.ActivityLevelId);
            targetCalories *= activityLevelValue.Value;

            if (user.FitnessGoal == FitnessGoalType.GainWeight)
            {
                targetCalories += 500;
            }
            else if (user.FitnessGoal == FitnessGoalType.LoseWeight)
            {
                targetCalories -= 500;
            }

            var targetCarbs = ((user.CarbsPercent * targetCalories / 100)) / 4;
            var targetProtein = ((user.ProteinPercent * targetCalories / 100)) / 4;
            var targetFats = ((user.FatsPercent * targetCalories / 100)) / 9;

            return new NutritionServiceModel
            {
                Calories = targetCalories,
                Carbs = targetCarbs,
                Protein = targetProtein,
                Fats = targetFats
            };
        }

        public async Task<IEnumerable<ActivityLevel>> GetActivityLevels()
            => await repo.All<ActivityLevel>().ToListAsync();

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
           
            var users = await this.repo.All<IdentityUser>().Select(u => new AllUsersViewModel()
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
            }).ToListAsync();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(repo.All<IdentityUser>().First(u => u.Id == user.Id));

                user.Roles = String.Join(", ", roles);
            }
      
            return users;
        }

        public string? GetById(string id)
        {
            var appUser = repo.AllReadonly<ApplicationUser>().FirstOrDefault(a => a.UserId == id);

            return appUser?.Id ?? null;
        }

        public async Task<NutritionServiceModel> GetUserTargetNutritionAsync(string userId)
        {
            var user = await repo
                .All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.TargetNutrients).FirstOrDefaultAsync();

            return new NutritionServiceModel()
            {
                Calories = user.TargetNutrients.Calories,
                Carbs = user.TargetNutrients.Carbohydrates,
                Protein = user.TargetNutrients.Proteins,
                Fats = user.TargetNutrients.Fats
            };
        }
    }
}
