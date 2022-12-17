using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Enums;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class FoodService : IFoodService
    {
        private readonly IRepository repo;

        public FoodService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddFood(FoodViewModel model, string? userId)
        {
            var food = new Food()
            {
                Name = model.Name,
                Type = model.Type,
                MeassureUnits = (MeassureUnitType)model.MeassureUnit,
                UserId = userId,
                Nutrition = new NutritionData()
                {
                    Calories = model.Calories,
                    Carbohydrates = model.Carbohydtrates,
                    Proteins = model.Proteins,
                    Fats = model.Fats
                }
            };

            await repo.AddAsync<Food>(food);
            await repo.SaveChangesAsync();
        }

        public async Task<string> DeleteAsync(string id)
        {
            var resultMessage = "Error! Unable to delete this item";
            var food = await LoadFood(id);

            if (food != null)
            {
                resultMessage = "Seccessfuly deleted item!";
                food.IsActive = false;
                await repo.SaveChangesAsync();
            }

            return resultMessage;
        }

        public async Task EditAsync(string Id, FoodViewModel model)
        {
            var food = await LoadFood(Id);
            
            if (food != null)
            {
                food.Name = model.Name;
                food.Type = model.Type;
                food.MeassureUnits = (MeassureUnitType)model.MeassureUnit;
                food.Nutrition.Calories = model.Calories;
                food.Nutrition.Carbohydrates = model.Carbohydtrates;
                food.Nutrition.Proteins = model.Proteins;
                food.Nutrition.Fats = model.Fats;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await repo.AllReadonly<Food>().Where(f => f.IsActive).AnyAsync(f => f.Id == id);
        }

        public async Task<FoodsQueryModel> GetAllAsync(string? userId = null,
            string? type = null, string? searchTerm = null,
            FoodSorting sorting = FoodSorting.PerName,
            int currentPage = 1,
            int foodsPerPage = 1)
        {
            var result = new FoodsQueryModel();

            IQueryable<Food> foods;

            foods = repo.AllReadonly<Food>().Include(f => f.Nutrition).Where(f => f.UserId == userId).Where(f => f.IsActive);

            if (string.IsNullOrEmpty(type) == false)
            {
                foods = foods.Where(f => f.Type == type);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                foods = foods
                    .Where(h => EF.Functions.Like(h.Name.ToLower(), searchTerm));
            }

            foods = sorting switch
            {
                FoodSorting.PerType => foods
                    .OrderBy(f => f.Type),
                FoodSorting.PerCalories => foods
                    .OrderByDescending(f => f.Nutrition.Calories),
                _ => foods.OrderBy(f => f.Name)
            };

            result.Foods = await foods
                .Skip((currentPage - 1) * foodsPerPage)
                .Take(foodsPerPage)
                .Select(f => new FoodServiceModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Type = f.Type,
                    MeassureUnit = (int)f.MeassureUnits,
                    Calories = f.Nutrition.Calories,
                    Carbohydtrates = f.Nutrition.Carbohydrates,
                    Proteins = f.Nutrition.Proteins,
                    Fats = f.Nutrition.Fats
                }).ToListAsync();

            result.TotalFoodsCount = await foods.CountAsync();

            return result;
        }

        public async Task<IEnumerable<string>> getAllTypesAsync()
        {
            return await repo.All<Food>().Where(f => f.IsActive).Select(f => f.Type).Distinct().ToListAsync();
        }

        public async Task<FoodViewModel> GetByIdAsync(string id)
        {
            var food = await LoadFood(id);

            return new FoodViewModel()
            {
                Name = food.Name,
                Type = food.Type,
                MeassureUnit = (int)food.MeassureUnits,
                Calories = food.Nutrition.Calories,
                Carbohydtrates = food.Nutrition.Carbohydrates,
                Proteins = food.Nutrition.Proteins,
                Fats = food.Nutrition.Fats
            };
        }

        public async Task<bool> FoodHasAppUser(string id)
        {
            var food = await repo.AllReadonly<Food>().FirstOrDefaultAsync(f => f.Id == id);

            if (food.UserId == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<FoodServiceModel>> LoadIngedientsAsync()
        {
            var foods = await repo.All<Food>().Where(f => f.IsActive).Include(f => f.Nutrition).ToListAsync();

            return foods.Select(f => new FoodServiceModel()
            {
                Id = f.Id,
                Name = f.Name,
                Type = f.Type,
                MeassureUnit = (int)f.MeassureUnits,
                Calories= f.Nutrition.Calories,
                Carbohydtrates= f.Nutrition.Carbohydrates,
                Proteins = f.Nutrition.Proteins,
                Fats = f.Nutrition.Fats
            });
        }

        private async Task<Food> LoadFood(string id)
        {
            return await repo.All<Food>()
                .Include(f => f.Nutrition)
                .Where(f => f.Id == id)
                .Where(f => f.IsActive)
                .FirstOrDefaultAsync();
        }
    }
}
