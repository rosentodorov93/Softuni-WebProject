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
            

            var guest = new ApplicationUser()
            {
                Id = "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                UserId = "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                FullName = "Pesho Petrov",
                Age = 29,
                Weight = 80,
                Height = 178,
                ActivityLevelId = 1,
                FitnessGoal = Enums.FitnessGoalType.MaintainWeight,
                Gender = Enums.Gender.Male,
                NutritionId = 1
            };

            users.Add(guest);

            return users;
        }
    }
}
