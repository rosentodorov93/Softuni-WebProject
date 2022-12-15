using FitnessDiary.Core.Models.Workout;

namespace FitnessDiary.Core.Contracts
{
    public interface IWorkoutService
    {
        Task<string> CreateTamplateAsync(CreateWorkoutViewModel model, string userId);
        Task<IEnumerable<ListingTamplateViewModel>> GetMineTamplatesAsync(string userId);
        Task<EditTamplateViewModel> GetTamplateById(string id);
        Task<string> AddExerciseToTamplateAsync(AddExerciseModel model);
        Task<string> RemoveExerciseAsync(string exerciseId, string tamplateId);
        Task<WorkoutViewModel> GetWorkoutByIdAsync(string id);
        Task EditTamplateAsync(EditTamplateViewModel model);
        Task AddToDiaryAsync(AddToDiaryViewModel model, string userId);
        Task<bool> TamplateExistsByIdAsync(string id);
        Task<bool> WorkoutExistsByIdAsync(string id);
        Task EditWorkoutAsync(WorkoutViewModel model);
        Task<AddToDiaryViewModel> GetTamplateForDiaryByIdAsync(string id);
        Task<string> DeleteAsync(string id);
    }
}
