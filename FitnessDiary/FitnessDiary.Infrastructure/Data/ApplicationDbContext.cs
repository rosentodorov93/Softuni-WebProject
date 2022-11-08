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
        public DbSet<ActivityLevel> ActivityLevels { get; set; }
        public DbSet<FitnessGoal> FitnessGoals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<NutritionData> Nutritions { get; set; }
        public DbSet<Serving> Servings { get; set; }
        public DbSet<DiaryDay> DiaryDays { get; set; }
        public DbSet<Diary> Diaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            builder.Entity<ActivityLevel>()
                .HasData(new ActivityLevel()
                {
                    Id = 1,
                    Type = "Low",
                    Value = 1.1
                },
                new ActivityLevel()
                {
                    Id = 2,
                    Type = "Medium",
                    Value = 1.2
                }, new ActivityLevel()
                {
                    Id = 3,
                    Type = "High",
                    Value = 1.3
                },
                new ActivityLevel()
                {
                    Id = 4,
                    Type = "Very High",
                    Value = 1.4
                });
            builder.Entity<FitnessGoal>()
                .HasData(new FitnessGoal()
                {
                    Id = 1,
                    Type = "Lose weight",
                },
                new FitnessGoal()
                {
                    Id = 2,
                    Type = "Gain weight",
                },
                new FitnessGoal()
                {
                    Id = 3,
                    Type = "Maintain weight",
                });

            base.OnModelCreating(builder);
        }
    }
}