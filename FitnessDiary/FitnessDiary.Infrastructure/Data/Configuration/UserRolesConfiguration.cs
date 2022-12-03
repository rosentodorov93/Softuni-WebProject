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
            },
            new IdentityUserRole<string>()
            {
                UserId = "9d6a8aea-aae9-44d4-ae4c-89f1236a96c4",
                RoleId = "312a798827de4362920a10e2a2b12e0c",
            });
        }
    }
}
