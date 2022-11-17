using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
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

            if (workout == null)
            {
                throw new ArgumentException("Workout is invalid");
            }

            var exercise = new ExerciseTamplate()
            {
                Name = model.ExerciseName,
                BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), model.BodyPart),
                SetCount = int.Parse(model.SetCount),
            };

            workout.Exercises.Add(exercise);

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

            tamplate.Exercises.Remove(exercise);

            await repo.SaveChangesAsync();
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
