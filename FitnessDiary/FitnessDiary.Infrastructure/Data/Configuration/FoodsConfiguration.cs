using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class FoodsConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasData(CreateFoods());
        }

        private List<Food> CreateFoods()
        {
            var foods = new List<Food>()
            {
                new Food()
                {
                    Id = "00c51d79-b0a2-44c4-9dfd-cc197f24c3e8",
                    Name = "Egg Size M",
                    MeassureUnits = Enums.MeassureUnitType.NutritionPerItem,
                    Type = "Protein",
                    NutritionId = 3
                },
                new Food()
                {
                    Id = "8070aa93-ea4c-477e-972b-aa3370f2d701",
                    Name = "Banana",
                    MeassureUnits = Enums.MeassureUnitType.NutritionPerItem,
                    Type = "Fruit",
                    NutritionId = 4
                },
                new Food()
                {
                    Id = "7bbc16e1-faa6-46ad-90ba-3dc038105ea2",
                    Name = "Potato",
                    MeassureUnits = Enums.MeassureUnitType.NutritionPer100g,
                    Type = "Vegetables",
                    NutritionId = 5
                },
            };

            return foods;
        }
    }
}
