﻿using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository repo;

        public WorkoutService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Ads exercise to created workout tamplate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddExerciseToTamplateAsync(AddExerciseModel model)
        {
            var workout = await LoadTamplate(model.WorkoutId);

            var exercise = new ExerciseTamplate()
            {
                Name = model.ExerciseName,
                BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), model.BodyPart),
                SetCount = model.SetCount,
            };

            if (workout.Exercises.Any(e => e.Name == exercise.Name))
            {
                throw new ArgumentException("Error! Exercise is already added");
            }

            workout.Exercises.Add(exercise);

            await repo.SaveChangesAsync();

            return $"Successfuly added {exercise.Name} to {workout.Name}";
        }
        /// <summary>
        /// Fills chosen tamplate with data and ads it to Diary
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddToDiaryAsync(AddToDiaryViewModel model, string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Diary)
                .ThenInclude(d => d.Workout)
                .ThenInclude(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync();

            var currentDay = user?.Diary.OrderBy(d => d.Id).Last();

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
            if (currentDay.Workout != null)
            {
                throw new InvalidOperationException("You allready have workout added");
            }
            var workout = new Workout()
            {
                Name = model.Name,
                Description = model.Description,
                Exercises = model.Exercises.Select(e => new Exercise()
                {
                    Name = e.Name,
                    BodyPart = (BodyPartType)e.BodyPart,
                    Sets = e.Sets.Select(s => new Set()
                    {
                        ExerciseId = e.Id,
                        Reps = s.Reps,
                        Load = s.Load
                    }).ToList(),
                }).ToList(),
            };

            currentDay.Workout = workout;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Crates new workout tamplate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> CreateTamplateAsync(CreateWorkoutViewModel model, string userId)
        {
            var workout = new WorkoutTamplate()
            {
                Name = model.Name,
                Description = model.Description,
                Exercises = model.Exercises.Select(e => new ExerciseTamplate()
                {
                    Name = e.Name,
                    BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), e.BodyPart),
                    SetCount = e.SetCount
                }).ToList(),
                UserId = userId
            };

            await repo.AddAsync(workout);
            await repo.SaveChangesAsync();

            return $"Creaated {model.Name}";
        }
        /// <summary>
        /// Deletes workout tamlate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string id)
        {
            var resultMessage = "Error! Unable to delete item";

            var tamplate = await repo.All<WorkoutTamplate>()
               .Where(t => t.IsActive)
               .Where(t => t.Id == id)
               .FirstOrDefaultAsync();

            if (tamplate != null)
            {
                tamplate.IsActive = false;
                resultMessage = $"Successfuly deleted {tamplate.Name}";
                await repo.SaveChangesAsync();
            }

            return resultMessage;
        }
        /// <summary>
        /// Edits workout tamplate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditTamplateAsync(EditTamplateViewModel model)
        {
            var tamplate = await LoadTamplate(model.Id);

            tamplate.Name = model.Name;
            tamplate.Description = model.Description;
            for (int i = 0; i < tamplate.Exercises.Count; i++)
            {
                var tamplateExercise = tamplate.Exercises[i];
                var modelExercise = model.Exercises[i];

                tamplateExercise.Name = modelExercise.Name;
                tamplateExercise.BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), modelExercise.BodyPart);
                tamplateExercise.SetCount = modelExercise.SetCount;
            }

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Edits added workout in diary
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditWorkoutAsync(WorkoutViewModel model)
        {
            var workout = await repo.All<Workout>()
                .Where(w => w.Id == model.Id)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync();

            if (workout != null)
            {
                for (int i = 0; i < model.Exercises.Count; i++)
                {
                    var workoutExercise = workout.Exercises[i];
                    var modelExercise = model.Exercises[i];

                    for (int j = 0; j < modelExercise.Sets.Count; j++)
                    {
                        var workoutSet = workoutExercise.Sets[j];
                        var modelSet = modelExercise.Sets[j];

                        workoutSet.Reps = modelSet.Reps;
                        workoutSet.Load = modelSet.Load;
                    }
                }

                await repo.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Returns user specified tamplates
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListingTamplateViewModel>> GetMineTamplatesAsync(string userId)
        {
            return await repo.All<WorkoutTamplate>()
                .Where(t => t.IsActive)
                .Where(t => t.UserId == userId)
                .Include(t => t.Exercises)
                .Select(t => new ListingTamplateViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Exercises = t.Exercises.Select(e => new ExerciseViewModel()
                    {
                        Name = e.Name,
                        BodyPart = e.BodyPart.ToString(),
                        SetCount = e.SetCount
                    }).ToArray()
                })
                .ToListAsync();
        }
        /// <summary>
        /// Returns tamplate for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditTamplateViewModel> GetTamplateById(string id)
        {
            var tamplate = await LoadTamplate(id);

            return new EditTamplateViewModel()
            {
                Id = tamplate.Id,
                Name = tamplate.Name,
                Description = tamplate.Description,
                Exercises = tamplate.Exercises.Select(e => new EditExerciseViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    BodyPart = e.BodyPart.ToString(),
                    SetCount = e.SetCount
                }).ToList()
            };
        }
        /// <summary>
        /// Returns tamplate for diary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AddToDiaryViewModel> GetTamplateForDiaryByIdAsync(string id)
        {
            var tamplate = await LoadTamplate(id);

            var result = new AddToDiaryViewModel()
            {
                Name = tamplate.Name,
                Description = tamplate.Description,
                Exercises = tamplate.Exercises.Select(e => new ExerciseWithSetsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    BodyPart = (int)e.BodyPart,
                    Sets = CreateSets(e.SetCount)
                }).ToList()
            };
        
            return result;
        }
        /// <summary>
        /// Returns workout from diary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WorkoutViewModel> GetWorkoutByIdAsync(string id)
        {
            var workout = await repo.All<Workout>()
                .Where(w => w.Id == id)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync();

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
                        Load = s.Load
                    }).ToList()
                }).ToList()
            };

        }
        /// <summary>
        /// Removes exercise from created workout tamplate
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <param name="tamplateId"></param>
        /// <returns></returns>
        public async Task<string> RemoveExerciseAsync(string exerciseId, string tamplateId)
        {
            var resultMessage = "Error! Unable to remove exercise!";
            var tamplate = await LoadTamplate(tamplateId);

            var exercise = tamplate.Exercises.FirstOrDefault(e => e.Id == exerciseId);

            if (exercise != null)
            {
                tamplate.Exercises.Remove(exercise);
                resultMessage = $"Successfuly removed {exercise.Name} from {tamplate.Name}";
                await repo.SaveChangesAsync();
            }

            return resultMessage;
        }
        /// <summary>
        /// Checks if tamplate exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> TamplateExistsByIdAsync(string id)
        {
            return await repo.AllReadonly<WorkoutTamplate>().Where(t => t.IsActive).AnyAsync(t => t.Id == id);
        }
        /// <summary>
        /// Cheks if workout exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> WorkoutExistsByIdAsync(string id)
        {
            return await repo.AllReadonly<Workout>().AnyAsync(t => t.Id == id);
        }

        private List<SetViewModel> CreateSets(int setCount)
        {
            var result = new List<SetViewModel>();

            for (int i = 0; i < setCount; i++)
            {
                result.Add(new SetViewModel());
            }

            return result;
        }
        private async Task<WorkoutTamplate> LoadTamplate(string id)
        {
            return await repo.All<WorkoutTamplate>()
                .Where(t => t.IsActive)
                .Where(t => t.Id == id)
                .Include(t => t.Exercises)
                .FirstOrDefaultAsync();
        }

    }
}
