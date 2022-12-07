using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
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

        public async Task AddExerciseToTamplateAsync(AddExerciseModel model)
        {
            var workout = await repo.All<WorkoutTamplate>()
                .Where(t => t.Id == model.WorkoutId)
                .Include(t => t.Exercises)
                .FirstOrDefaultAsync();


            var exercise = new ExerciseTamplate()
            {
                Name = model.ExerciseName,
                BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), model.BodyPart),
                SetCount = model.SetCount,
            };

            workout.Exercises.Add(exercise);

            await repo.SaveChangesAsync();
        }

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

            var workout = new Workout()
            {
                Name = model.Name,
                Description = model.Description,
                Exercises = model.Exercises.Select(e => new Exercise()
                {
                    Name = e.Name,
                    BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), e.BodyPart),
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

        public async Task EditTamplateAsync(EditTamplateViewModel model)
        {
            var tamplate = await repo.All<WorkoutTamplate>().Where(t => t.Id == model.Id).Include(t => t.Exercises).FirstOrDefaultAsync();

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

        public async Task<IEnumerable<ListingTamplateViewModel>> GetMineTamplatesAsync(string userId)
        {
            return await repo.All<WorkoutTamplate>()
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

        public async Task<EditTamplateViewModel> GetTamplateById(string id)
        {
            var tamplate = await repo.All<WorkoutTamplate>()
                .Where(t => t.Id == id)
                .Include(t => t.Exercises)
                .FirstOrDefaultAsync();

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

        public async Task<AddToDiaryViewModel> GetTamplateForDiaryByIdAsync(string id)
        {
            var tamplate = await repo.All<WorkoutTamplate>()
               .Where(t => t.Id == id)
               .Include(t => t.Exercises)
               .FirstOrDefaultAsync();

            var result = new AddToDiaryViewModel()
            {
                Name = tamplate.Name,
                Description = tamplate.Description,
                Exercises = tamplate.Exercises.Select(e => new ExerciseWithSetsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    BodyPart = e.BodyPart.ToString(),
                    Sets = CreateSets(e.SetCount)
                }).ToList()
            };
        
            return result;
        }

        public async Task RemoveExerciseAsync(string exerciseId, string tamplateId)
        {
            var tamplate = await repo.All<WorkoutTamplate>()
                .Where(t => t.Id == tamplateId)
                .Include(t => t.Exercises)
                .FirstOrDefaultAsync();

            var exercise = tamplate.Exercises.FirstOrDefault(e => e.Id == exerciseId);

            if (exercise != null)
            {
                tamplate.Exercises.Remove(exercise);

                await repo.SaveChangesAsync();
            }

            
        }

        public async Task<bool> TamplateExistsByIdAsync(string id)
        {
            return await repo.AllReadonly<WorkoutTamplate>().AnyAsync(t => t.Id == id);
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

    }
}
