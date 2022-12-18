using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>()
            {
                UserId = "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                RoleId = "cd1439f9-201b-42ac-96d2-5f13fd35ad5a",
            },
            new IdentityUserRole<string>()
            {
                UserId = "02b52032-ec58-496e-b58e-0533479ff27d",
                RoleId = "6a651666-0353-4a96-b3eb-d6b78010b6ba",
            });
        }
    }
}
