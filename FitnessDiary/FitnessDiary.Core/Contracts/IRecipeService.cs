using FitnessDiary.Core.Models.Recepie;

namespace FitnessDiary.Core.Contracts
{
    public interface IRecipeService
    {
        Task<string> AddAsync(CreateRecipeModel model);
        Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, string recepieId);
        Task<DetailsViewModel> GetDetailsByIdAsync(string id);
        Task<EditViewModel> GetByIdAsync(string id);
        Task<IEnumerable<RecipeListingViewModel>> GetAllByUserId(string userId);
        Task<IEnumerable<IngredientDetailsViewModel>> GetIngredientsAsync(string id);
        Task RemoveIngredient(string recipeid, int ingredientToRemove);
        Task<DetailsViewModel> EditAsync(EditViewModel model);
        Task<bool> ExistsByIdAsync(string id);
        Task<string> DeleteAsync(string id);
    }
}
