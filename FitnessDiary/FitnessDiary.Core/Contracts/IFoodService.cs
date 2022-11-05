using FitnessDiary.Core.Models.Enums;
using FitnessDiary.Core.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IFoodService
    {
        Task<IEnumerable<FoodViewModel>> GetAllAsync();
        Task AddFood(FoodViewModel model);
        Task AddToCollectionAsync(string? userId, string foodId);
        Task<MinePageViewModel> GetAllById(string? userId,
            string type = null,
            string searchTerm = null,
            FoodSorting sorting = FoodSorting.PerName,
            int currentPage = 1,
            int foodsPerPage = int.MaxValue);
        Task<IEnumerable<string>> getAllTypesAsync();
       
    }
}
