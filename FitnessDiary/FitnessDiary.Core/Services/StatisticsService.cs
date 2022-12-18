using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Statistics;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repo;

        public StatisticsService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Returns data on calorie eaten and load liften in workouts for the past 6 days
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<StatisticsServiceModel> Total(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Diary)
                .ThenInclude(d => d.Nutrition)
                .Include(u => u.Diary)
                .ThenInclude(d => d.Workout)
                .ThenInclude(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync();

            var result = new StatisticsServiceModel();

            var currentDate = DateTime.Now.Date;
            var startDate = currentDate.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                var diaryDay = user?.Diary.FirstOrDefault(d => d.DateTime.Date == startDate);
                var label = startDate.DayOfWeek.ToString();
                double totalCaloriesForDay = 0;
                double totalWorkoutLoad = 0;

                if (diaryDay != null)
                {
                    totalCaloriesForDay = diaryDay.Nutrition.Calories;
                    totalWorkoutLoad = diaryDay.Workout == null ? 0 : CalculateWorkoutLoad(diaryDay.Workout);
                }
                result.Labels[i] = label;
                result.Calories[i] = totalCaloriesForDay;
                result.Workouts[i] = totalWorkoutLoad;

                startDate = startDate.AddDays(1);
            }

            return result;
        }

        private double CalculateWorkoutLoad(Workout workout)
        {
            double load = 0;

            foreach (var exercise in workout.Exercises)
            {
                foreach (var set in exercise.Sets)
                {
                    load += set.Reps * set.Load;
                }
            }

            return load;
        }
    }
}
