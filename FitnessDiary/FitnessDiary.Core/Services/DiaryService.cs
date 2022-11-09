using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IRepository repo;

        public DiaryService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<DiaryDayServiceModel> GetByIdAsync(string userId)
        {
            var user = await repo.All<ApplicationUser>().Where(u => u.Id == userId)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Nutrition)
                .FirstOrDefaultAsync();

            if (user.Diary.Count == 0)
            {
                var diaryDay = new DiaryDay()
                {
                    DateTime = DateTime.Now.Date,
                    Nutrition = new NutritionData()
                };
                user.Diary.Add(diaryDay);
            }

            var currentDay = user.Diary.OrderBy(x => x.Id).Last();

            List<ServingViewModel> breakfastServings = GetServings(currentDay, ServingCategory.Breakfast);
            List<ServingViewModel> lunchServings = GetServings(currentDay, ServingCategory.Lunch);
            List<ServingViewModel> dinnerServings = GetServings(currentDay, ServingCategory.Dinner);
            List<ServingViewModel> snackServings = GetServings(currentDay, ServingCategory.Snack);

            CalculateDayNutrition(currentDay);

            await repo.SaveChangesAsync();

            return new DiaryDayServiceModel()
            {

                BreakfastServings = breakfastServings,
                LunchServings = lunchServings,
                DinnerServings = dinnerServings,
                SnackServings = snackServings,
                Date = currentDay.DateTime.Date,
                Nutrition = new NutritionServiceModel()
                {
                    Calories = currentDay.Nutrition.Calories,
                    Carbs = currentDay.Nutrition.Carbohydrates,
                    Protein = currentDay.Nutrition.Proteins,
                    Fats = currentDay.Nutrition.Fats,
                }

            };
        }

        private void CalculateDayNutrition(DiaryDay currentDiaryDay)
        {
            foreach (var serving in currentDiaryDay.Servings)
            {
                currentDiaryDay.Nutrition.Calories += serving.Nutrition.Calories * serving.Amount;
                currentDiaryDay.Nutrition.Carbohydrates += serving.Nutrition.Carbohydrates * serving.Amount;
                currentDiaryDay.Nutrition.Proteins += serving.Nutrition.Proteins * serving.Amount;
                currentDiaryDay.Nutrition.Fats += serving.Nutrition.Fats * serving.Amount;
            }
        }

        private List<ServingViewModel> GetServings(DiaryDay currentDay, ServingCategory category)
        {
            return currentDay.Servings
                .Where(s => s.Category == category)
                .Select(s => new ServingViewModel()
                {
                    Name = s.Name,
                    Category = s.Category.ToString(),
                    Amount = s.Amount,
                    Nutrition = new NutritionServiceModel()
                    {
                        Calories = s.Nutrition.Calories,
                        Carbs = s.Nutrition.Carbohydrates,
                        Protein = s.Nutrition.Proteins,
                        Fats = s.Nutrition.Fats
                    }

                }).ToList();
        }
    }
}

