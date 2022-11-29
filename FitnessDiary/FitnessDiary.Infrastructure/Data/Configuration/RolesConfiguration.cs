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
                     
            };

            return roles;
        }
    }
}
