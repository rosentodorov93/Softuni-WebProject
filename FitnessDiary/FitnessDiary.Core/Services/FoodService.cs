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

        public async Task AddFood(FoodViewModel model, string userId)
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

        public async Task DeleteAsync(string id)
        {
            var food = await LoadFood(id);

            if (food != null)
            {
                food.IsActive = false;
                await repo.SaveChangesAsync();
            }
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

            foods = repo.AllReadonly<Food>().Where(f => f.UserId == userId).Where(f => f.IsActive);

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
                FoodSorting.PerCalories => foods
                    .OrderByDescending(h => h.Nutrition.Calories),
                FoodSorting.PerType => foods
                    .OrderBy(f => f.Type),
                _ => foods.OrderBy(f => f.Name)
            };

            result.Foods = await foods
                .Include(f => f.Nutrition)
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

        //public async Task<MinePageViewModel> GetAllById(string? userId = null,
        //    string? type = null, string? searchTerm = null,
        //    FoodSorting sorting = FoodSorting.PerName,
        //    int currentPage = 1,
        //    int foodsPerPage = int.MaxValue)
        //{
        //    var user = await repo.All<ApplicationUser>().Include(u => u.Foods).ThenInclude(f => f.Nutrition).FirstOrDefaultAsync(u => u.Id == userId);

        //    if (user == null)
        //    {
        //        throw new ArgumentException("Invalid user ID");
        //    }

        //    var userFoods = user.Foods.Select(f => new FoodServiceModel()
        //    {
        //        Id = f.Id,
        //        Name = f.Name,
        //        Type = f.Type,
        //        MeassureUnit = (int)f.MeassureUnits,
        //        Calories = f.Nutrition.Calories,
        //        Carbohydtrates = f.Nutrition.Carbohydrates,
        //        Proteins = f.Nutrition.Proteins,
        //        Fats = f.Nutrition.Fats
        //    });

        //    if (type != null)
        //    {
        //        userFoods = userFoods.Where(f => f.Type == type);
        //    }

        //    if (searchTerm != null)
        //    {
        //        userFoods = userFoods.Where(f => f.Name.ToLower().Contains(searchTerm.ToLower()));
        //    }

        //    userFoods = sorting switch
        //    {
        //        FoodSorting.PerCalories => userFoods.OrderByDescending(c => c.Calories),
        //        FoodSorting.PerType => userFoods.OrderBy(c => c.Type),
        //        FoodSorting.PerName or _ => userFoods.OrderByDescending(c => c.Name)
        //    };

        //    var totalFoods = userFoods.Count();

        //    userFoods = userFoods.Skip((currentPage - 1) * foodsPerPage).Take(foodsPerPage);

        //    return new MinePageViewModel()
        //    {
        //        TotalFoods = totalFoods,
        //        CurrentPage = currentPage,
        //        FoodsPerPage = foodsPerPage,
        //        Foods = userFoods,
        //    };
        //}

        public async Task<IEnumerable<string>> getAllTypesAsync()
            => await repo.All<Food>().Select(f => f.Type).Distinct().ToListAsync();

        public async Task<FoodViewModel> GetByIdAsync(string id)
        {
            var food = await repo.All<Food>()
                .Include(f => f.Nutrition)
                .Where(f => f.IsActive)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync(f => f.Id == id);

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
