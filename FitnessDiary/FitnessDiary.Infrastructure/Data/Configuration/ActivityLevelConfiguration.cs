using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class ActivityLevelConfiguration : IEntityTypeConfiguration<ActivityLevel>
    {
        public void Configure(EntityTypeBuilder<ActivityLevel> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<ActivityLevel> CreateCategories()
        {
            List<ActivityLevel> categories = new List<ActivityLevel>()
            {
                new ActivityLevel()
                {
                    Id = 1,
                    Type = "Light ",
                    Value = 1.375
                },
                new ActivityLevel()
                {
                    Id = 2,
                    Type = "Moderate ",
                    Value = 1.55
                }, new ActivityLevel()
                {
                    Id = 3,
                    Type = "Very Active",
                    Value = 1.725
                },
                new ActivityLevel()
                {
                    Id = 4,
                    Type = "Extra Active",
                    Value = 1.9
                }
            };

            return categories;
        }
    }
}
