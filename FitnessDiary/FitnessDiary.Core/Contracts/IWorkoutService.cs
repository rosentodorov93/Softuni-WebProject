using FitnessDiary.Core.Models.Workout;

namespace FitnessDiary.Core.Contracts
{
    public interface IWorkoutService
    {
        Task CreateAsync(CreateWorkoutViewModel model);
        Task<IEnumerable<CreateWorkoutViewModel>> GetMineAsync();
    }
}
