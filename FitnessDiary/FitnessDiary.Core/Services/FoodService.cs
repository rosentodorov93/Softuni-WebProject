using FitnessDiary.Core.Contracts;
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

        public async Task<IEnumerable<FoodViewModel>> GetAllById(string? userId)
        {
            var user = await repo.All<ApplicationUser>().Include(u => u.Foods).ThenInclude(f => f.Nutrition).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.Foods.Select(f => new FoodViewModel()
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
        }
    }
}
