using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;

        public AccountService(IRepository _repo)
        {
            repo = _repo;
        }

        public NutritionServiceModel CalculateTargetNutrientsAsync(ApplicationUser user)
        {
            double targetCalories = 0;

            if(user.Gender == Gender.Male)
            {
                targetCalories = 66.5 + (13.75 * user.Weight) + (5.003 * user.Height) - (6.75 * user.Age);
            }
            else
            {
                targetCalories = 655.1 + (9.563 * user.Weight) + (1.850 * user.Height) - (4.676 * user.Age);
            }

            targetCalories *= user.ActivityLevel;

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
