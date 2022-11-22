using FitnessDiary.Infrastructure.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var guest = new ApplicationUser()
            {
                Id = "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                UserName = "guest",
                NormalizedUserName = "guest",
                FullName = "Pesho Petrov",
                Age = 29,
                Weight = 80,
                Height = 178,
                ActivityLevelId = 1,
                FitnessGoal = Enums.FitnessGoalType.MaintainWeight,
                Gender = Enums.Gender.Male,
                NutritionId = 1
            };

            guest.PasswordHash =
                 hasher.HashPassword(guest, "guest123");

            users.Add(guest);

            var admin = new ApplicationUser()
            {
                Id = "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                UserName = "admin",
                NormalizedUserName = "admin",
                FullName = "Admin",
                Age = 30,
                Weight = 85,
                Height = 185,
                ActivityLevelId = 3,
                FitnessGoal = Enums.FitnessGoalType.GainWeight,
          
                Gender = Enums.Gender.Male,
                NutritionId = 2
                
            };

            admin.PasswordHash =
                 hasher.HashPassword(admin, "admin123");

            users.Add(admin);

            return users;
        }
    }
}
