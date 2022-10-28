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
        public Task<IEnumerable<FoodViewModel>> GetAllAsync();
        Task AddFood(FoodViewModel model);
        Task AddToCollectionAsync(string? userId, string foodId);
        public Task<IEnumerable<FoodViewModel>> GetAllById(string? userId);
    }
}
