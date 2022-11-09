using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<ActivityLevel>> GetActivityLevels();
        NutritionServiceModel CalculateTargetNutrientsAsync(ApplicationUser user);
        Task<NutritionServiceModel> GetUserTargetNutritionAsync(string userId);
    }
}
