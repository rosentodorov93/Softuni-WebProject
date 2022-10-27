using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Food;
using FitnessDiary.Infrastructure.Data;
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
        private readonly ApplicationDbContext context;

        public FoodService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddFood(FoodViewModel model)
        {
            var food = new Food()
            {
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

            await context.Foods.AddAsync(food);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FoodViewModel>> GetAllAsync()
        {
            var foods = await context.Foods.Include(f => f.Nutrition).ToListAsync();

            return foods.Select(f => new FoodViewModel()
            {
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
