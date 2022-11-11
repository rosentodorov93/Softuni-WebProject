using FitnessDiary.Infrastructure.Data.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ActivityLevel> ActivityLevels { get; set; } = null!;
        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        
        public DbSet<NutritionData> Nutritions { get; set; } = null!;
        public DbSet<Serving> Servings { get; set; } = null!;
        public DbSet<DiaryDay> DiaryDays { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            builder.Entity<ActivityLevel>()
                .HasData(new ActivityLevel()
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
                });

            base.OnModelCreating(builder);
        }
    }
}