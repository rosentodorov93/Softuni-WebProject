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
                    Id = "312a798827de4362920a10e2a2b12e0c",
                    ConcurrencyStamp = "beb7ec18c2d24b65b6c4e2cfaf0bb03b",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole()
                {
                    Id = "6a651666-0353-4a96-b3eb-d6b78010b6ba",
                    ConcurrencyStamp = "a0827383-cf6f-4400-bc86-bdaf2dc35765",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },

            };

            return roles;
        }
    }
}
