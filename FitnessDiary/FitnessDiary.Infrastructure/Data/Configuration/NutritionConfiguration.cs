using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class NutritionConfiguration : IEntityTypeConfiguration<NutritionData>
    {
        public void Configure(EntityTypeBuilder<NutritionData> builder)
        {
            builder.HasData(CreateNutritions());
        }

        private List<NutritionData> CreateNutritions()
        {
            var nutritions = new List<NutritionData>()
           {
               new NutritionData()
                {
                    Id = 1,
                    Calories = 2500,
                    Carbohydrates = 245,
                    Proteins = 105,
                    Fats = 45
                },
               new NutritionData()
                {
                    Id = 2,
                    Calories = 3500,
                    Carbohydrates = 275,
                    Proteins = 145,
                    Fats = 65
                },
               new NutritionData()
                    {
                        Id = 3,
                        Calories = 66,
                        Carbohydrates = 0.3,
                        Proteins = 6.4,
                        Fats = 4.6
                    },
               new NutritionData()
                    {
                        Id = 4,
                        Calories = 89,
                        Carbohydrates = 23,
                        Proteins = 1,
                        Fats = 0.3
                    },
                new NutritionData()
                    {
                        Id = 5,
                        Calories = 77,
                        Carbohydrates = 17,
                        Proteins = 2,
                        Fats = 0.1
                    }
           };

            return nutritions;
        }
    }
}
