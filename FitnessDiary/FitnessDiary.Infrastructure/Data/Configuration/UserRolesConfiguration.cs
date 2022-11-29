using FitnessDiary.Infrastructure.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>()
            {
                UserId = "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                RoleId = "cd1439f9-201b-42ac-96d2-5f13fd35ad5a",
            });
        }
    }
}
