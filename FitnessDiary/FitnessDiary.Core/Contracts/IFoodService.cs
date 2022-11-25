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
        Task<FoodsQueryModel> GetAllAsync(
            string? userId = null,
            string? category = null, 
            string? searchTerm = null,
            FoodSorting sorting = FoodSorting.PerName,
            int currentPage = 1,
            int housesPerPage = 1
            );
        Task AddFood(FoodViewModel model, string userId);
        Task<IEnumerable<FoodServiceModel>> LoadIngedientsAsync();

        ////Task<MinePageViewModel> GetAllById(string? userId,
        //    string? type = null,
        //    string? searchTerm = null,
        //    FoodSorting sorting = FoodSorting.PerName,
        //    int currentPage = 1,
        //    int foodsPerPage = 1);
        Task<IEnumerable<string>> getAllTypesAsync();
        Task<bool> ExistsByIdAsync(string id);
        Task<FoodViewModel> GetByIdAsync(string id);
        Task EditAsync(string Id, FoodViewModel model);
        Task DeleteAsync(string id);
    }
}
