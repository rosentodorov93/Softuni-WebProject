using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Enums;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Services
{
    public class FoodService: IFoodService
    {
        private readonly IRepository repo;

        public FoodService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddFood(FoodViewModel model)
        {
            var food = new Food()
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                MeassureUnits = (MeassureUnitType)model.MeassureUnit,
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

        public async Task AddToCollectionAsync(string? userId, string foodId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Foods)
                .ThenInclude(f => f.Nutrition)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var food = await repo.All<Food>().FirstOrDefaultAsync(f => f.Id == foodId);

            if (food == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }

            if (!user.Foods.Any(f => f.Id == foodId))
            {
                user.Foods.Add(food);
                await repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FoodViewModel>> GetAllAsync()
        {
            var foods = await repo.All<Food>().Include(f => f.Nutrition).ToListAsync();

            return foods.Select(f => new FoodViewModel()
            {
                Id= f.Id,
                Name = f.Name,
                Type = f.Type,
                MeassureUnit = (int)f.MeassureUnits,
                Calories = f.Nutrition.Calories,
                Carbohydtrates = f.Nutrition.Carbohydrates,
                Proteins = f.Nutrition.Proteins,
                Fats = f.Nutrition.Fats
            });
        }

        public async Task<MinePageViewModel> GetAllById(string? userId,
            string type = null, string searchTerm = null,
            FoodSorting sorting = FoodSorting.PerName,
            int currentPage = 1,
            int foodsPerPage = int.MaxValue)
        {
            var user = await repo.All<ApplicationUser>().Include(u => u.Foods).ThenInclude(f => f.Nutrition).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var userFoods = user.Foods.Select(f => new FoodViewModel()
            {
                Id = f.Id,
                Name = f.Name,
                Type = f.Type,
                MeassureUnit = (int)f.MeassureUnits,
                Calories = f.Nutrition.Calories,
                Carbohydtrates = f.Nutrition.Carbohydrates,
                Proteins = f.Nutrition.Proteins,
                Fats = f.Nutrition.Fats
            });
            
            if (type != null)
            {
                userFoods = userFoods.Where(f => f.Type == type);
            }

            if (searchTerm != null)
            {
                userFoods = userFoods.Where(f => f.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            userFoods = sorting switch
            {
                FoodSorting.PerCalories => userFoods.OrderByDescending(c => c.Calories),
                FoodSorting.PerType => userFoods.OrderBy(c => c.Type),
                FoodSorting.PerName or _ => userFoods.OrderByDescending(c => c.Name)
            };

            var totalFoods = userFoods.Count();

            userFoods = userFoods.Skip((currentPage - 1) * foodsPerPage).Take(foodsPerPage);

            return new MinePageViewModel()
            {
                TotalFoods = totalFoods,
                CurrentPage = currentPage,
                FoodsPerPage = foodsPerPage,
                Foods = userFoods,
            };
        }

        public async Task<IEnumerable<string>> getAllTypesAsync()
            => await repo.All<Food>().Select(f => f.Type).Distinct().ToListAsync();

      
    }
}
