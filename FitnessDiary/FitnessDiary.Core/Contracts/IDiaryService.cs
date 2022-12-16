using FitnessDiary.Core.Models.Diary;

namespace FitnessDiary.Core.Contracts
{
    public interface IDiaryService
    {
        Task<DiaryDayServiceModel> LoadDiaryDay(string userId);
        Task<string> AddFoodServingAsync(string userId, string id, double amount, string category);
        Task<string> AddRecipeServingAsync(string userId, string id, double amount, string category);
        Task RemoveServingAsync(string userId, int id);
    }
}
