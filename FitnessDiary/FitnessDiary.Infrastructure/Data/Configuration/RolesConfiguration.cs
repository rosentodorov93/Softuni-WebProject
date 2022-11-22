using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(CreateRoles());
        }

        private List<IdentityRole> CreateRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "cd1439f9-201b-42ac-96d2-5f13fd35ad5a",
                    ConcurrencyStamp = "0d1b21f4-ad82-4365-83e4-889624bd0626",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                     new IdentityRole()
                {
                    Id = "930c94ba-f945-473c-bb6b-6b1298a44b85",
                    ConcurrencyStamp = "5107fd53-7c15-4130-b226-5c60b0cf308f",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
            };

            return roles;
        }
    }
}
