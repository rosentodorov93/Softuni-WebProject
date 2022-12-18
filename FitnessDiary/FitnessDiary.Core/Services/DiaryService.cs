using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IRepository repo;

        public DiaryService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Ads food record in diary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddFoodServingAsync(string userId, string id, double amount, string category)
        {
            var food = await repo.All<Food>()
                .Where(f => f.Id == id)
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync();

            if (food == null)
            {
                throw new ArgumentException("Invalid foodId");
            }

            var user = await repo.All<ApplicationUser>().Where(u => u.Id == userId)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Servings)
                .ThenInclude(d => d.Nutrition)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid userId");
            }

            var currentDay = user?.Diary.OrderBy(x => x.Id).LastOrDefault();

            if (currentDay == null || currentDay.DateTime.Date < DateTime.Today)
            {
                var diaryDay = new DiaryDay()
                {
                    DateTime = DateTime.Today,
                    Nutrition = new NutritionData()
                };
                user?.Diary.Add(diaryDay);
                currentDay = diaryDay;
            }

            if (currentDay.Servings.Any(s => s.Name == food.Name))
            {
                throw new ArgumentException($"You allready added {food.Name}");
            }

            currentDay.Servings.Add(new Serving()
            {
                Name = food.Name,
                Amount = amount,
                Nutrition = food.Nutrition,
                Category = (ServingCategory)Enum.Parse(typeof(ServingCategory), category, true),
                DiaryDay = currentDay
            });

            await repo.SaveChangesAsync();
            return $"Successfully added {amount} {food.Name} to {category}";
        }
        /// <summary>
        /// Ads food record in diary from recipes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddRecipeServingAsync(string userId, string id, double amount, string category)
        {
            
            var recipe = await repo.All<Recipe>()
                .Where(r => r.Id == id)
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                throw new ArgumentException("Invalid recipe Id");
            }

            var user = await repo.All<ApplicationUser>().Where(u => u.Id == userId)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Nutrition)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid userId");
            }

            var currentDay = user?.Diary.OrderBy(x => x.Id).LastOrDefault();

            if (currentDay == null || currentDay.DateTime.Date < DateTime.Today)
            {
                var diaryDay = new DiaryDay()
                {
                    DateTime = DateTime.Today,
                    Nutrition = new NutritionData()
                };
                user?.Diary.Add(diaryDay);
                currentDay = diaryDay;
            }

            if (currentDay.Servings.Any(s => s.Name == recipe.Name))
            {
                throw new ArgumentException($"You allready added {recipe.Name}");
            }

            currentDay.Servings.Add(new Serving()
            {
                Name = recipe.Name,
                Amount = amount,
                Nutrition = recipe.Nutrition,
                Category = (ServingCategory)Enum.Parse(typeof(ServingCategory), category, true),
                DiaryDay = currentDay
            });

            await repo.SaveChangesAsync();
            return $"Successfully added {amount} portions {recipe.Name} to {category}";
        }
        /// <summary>
        /// Returns main diary information for today
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DiaryDayServiceModel> LoadDiaryDay(string userId)
        {
            var user = await repo.All<ApplicationUser>().Where(u => u.Id == userId)
                .Include(x => x.Diary)
                .ThenInclude(x => x.Servings)
                .ThenInclude(s => s.Nutrition)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Nutrition)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Workout)
                .ThenInclude(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync();

            var currentDay = user?.Diary.OrderBy(x => x.Id).LastOrDefault();

            if (currentDay == null || currentDay.DateTime.Date < DateTime.Today)
            {
                var diaryDay = new DiaryDay()
                {
                    DateTime = DateTime.Today,
                    Nutrition = new NutritionData()
                };
                user?.Diary.Add(diaryDay);
                currentDay = diaryDay;
            }

            List<ServingViewModel> breakfastServings = GetServings(currentDay, ServingCategory.Breakfast);
            List<ServingViewModel> lunchServings = GetServings(currentDay, ServingCategory.Lunch);
            List<ServingViewModel> dinnerServings = GetServings(currentDay, ServingCategory.Dinner);
            List<ServingViewModel> snackServings = GetServings(currentDay, ServingCategory.Snack);

            CalculateDayNutrition(currentDay);
             
            await repo.SaveChangesAsync();
            
            return new DiaryDayServiceModel()
            {
                Id = currentDay.Id,
                BreakfastServings = breakfastServings,
                LunchServings = lunchServings,
                DinnerServings = dinnerServings,
                SnackServings = snackServings,
                Date = currentDay.DateTime.Date,
                Workout = LoadWorkout(currentDay.Workout),
                Nutrition = new NutritionServiceModel()
                {
                    Calories = currentDay.Nutrition.Calories,
                    Carbs = currentDay.Nutrition.Carbohydrates,
                    Protein = currentDay.Nutrition.Proteins,
                    Fats = currentDay.Nutrition.Fats,
                }

            };
        }

        private WorkoutViewModel LoadWorkout(Workout workout)
        {

            if (workout != null)
            {
                return new WorkoutViewModel()
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    Description = workout.Description,
                    Exercises = workout.Exercises.Select(e => new ExerciseWithSetsViewModel()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        BodyPart = (int)e.BodyPart,
                        Sets = e.Sets.Select(s => new SetViewModel()
                        {
                            Reps = s.Reps,
                            Load = s.Load,
                        }).ToList()
                    }).ToList(),
                };
            }
            return null;
        }
        /// <summary>
        /// Remove food record from diary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task RemoveServingAsync(string userId, int id)
        {
            var user = await repo.All<ApplicationUser>().Where(u => u.Id == userId)
                .Include(x => x.Diary)
                .ThenInclude(d => d.Servings)
                .ThenInclude(d => d.Nutrition)
                .FirstOrDefaultAsync();


            var currentDay = user?.Diary.OrderBy(x => x.Id).LastOrDefault();

            if (currentDay == null || currentDay.DateTime.Date < DateTime.Today)
            {
                var diaryDay = new DiaryDay()
                {
                    DateTime = DateTime.Today,
                    Nutrition = new NutritionData()
                };
                user?.Diary.Add(diaryDay);
                currentDay = diaryDay;
            }

            var serving = currentDay.Servings.FirstOrDefault(s => s.Id == id);

            if (serving == null)
            {
                throw new ArgumentException("Invalid serving Id");
            }

            currentDay.Servings.Remove(serving);
            await repo.SaveChangesAsync();
        }

        private void CalculateDayNutrition(DiaryDay currentDiaryDay)
        {
            currentDiaryDay.Nutrition.Calories = 0;
            currentDiaryDay.Nutrition.Carbohydrates = 0;
            currentDiaryDay.Nutrition.Proteins = 0;
            currentDiaryDay.Nutrition.Fats = 0;

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
                    Id = s.Id,
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

