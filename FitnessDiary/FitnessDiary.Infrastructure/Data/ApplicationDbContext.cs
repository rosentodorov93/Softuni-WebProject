using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Configuration;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
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
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<NutritionData> Nutritions { get; set; } = null!;
        public DbSet<Serving> Servings { get; set; } = null!;
        public DbSet<DiaryDay> DiaryDays { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Workout> Workouts { get; set; } = null!;
        public DbSet<WorkoutTamplate> WorkoutTamplates { get; set; } = null!;
        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<ExerciseTamplate> ExerciseTamplates { get; set; } = null!;
        public DbSet<Set> Sets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new ActivityLevelConfiguration());
            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new NutritionConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());
            builder.ApplyConfiguration(new FoodsConfiguration());

            base.OnModelCreating(builder);
        }
    }
}