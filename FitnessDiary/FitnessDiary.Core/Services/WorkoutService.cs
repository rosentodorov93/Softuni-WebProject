using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;

namespace FitnessDiary.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository repo;

        public WorkoutService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task CreateAsync(CreateWorkoutViewModel model)
        {
            var workout = new Workout()
            {
                Name = model.Name,
                Description = model.Description,
            };

            for (int i = 0; i < model.Exercises.Length; i++)
            {
                var currenExercise = model.Exercises[i];

                workout.Exercises.Add(new Exercise()
                {
                    Name = currenExercise.Name,
                    BodyPart = (BodyPartType)Enum.Parse(typeof(BodyPartType), currenExercise.BodyPart, true),
                    Sets = new Set[currenExercise.SetCount]
                });
            }

            await repo.AddAsync(workout);
            await repo.SaveChangesAsync();
        }

        public Task<IEnumerable<CreateWorkoutViewModel>> GetMineAsync()
        {
            throw new NotImplementedException();
        }
    }
}
