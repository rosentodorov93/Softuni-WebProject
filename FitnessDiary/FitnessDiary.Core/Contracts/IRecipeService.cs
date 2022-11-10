using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IRecipeService
    {
        Task<string> AddAsync(CreateViewModel model);
        Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, string recepieId);
        Task<DetailsViewModel> GetByIdAsync(string id);
        Task<IEnumerable<RecipeListingViewModel>> GetAllById(string? userId);
        Task<IEnumerable<IngredientDetailsViewModel>> GetIngredientsAsync(string id);
        Task RemoveIngredient(string recipeid, int ingredientToRemove);
        Task<DetailsViewModel> EditAsync(EditViewModel model);
    }
}
