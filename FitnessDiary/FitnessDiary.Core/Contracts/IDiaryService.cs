﻿using FitnessDiary.Core.Models.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IDiaryService
    {
        Task<DiaryDayServiceModel> GetByIdAsync(string userId);
        Task AddFoodServingAsync(string userId, string id, double amount, string category);
        Task<List<FoodDiaryServiceModel>> GetFoodsFromDbAsync();
        Task<List<FoodDiaryServiceModel>> GetMineFoodsFromDbAsync(string userId);
        Task<List<FoodDiaryServiceModel>> GetRecipesFromDbAsync(string userId);
        Task AddRecipeServingAsync(string userId, string id, double amount, string category);
        Task RemoveServingAsync(string userId, int id);
    }
}
