using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(CreateIdentityUsers());
        }

        private List<IdentityUser> CreateIdentityUsers()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<IdentityUser>();

            var guest = new IdentityUser()
            {
                Id = "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                Email = "guest@mail.bg",
                UserName = "guest",
                NormalizedEmail = "GUEST@MAIL.BG",
                NormalizedUserName = "GUEST"
            };

            guest.PasswordHash = hasher.HashPassword(guest, "guest123");

            users.Add(guest);

            var admin = new IdentityUser()
            {
                Id = "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                Email = "admin@mail.bg",
                UserName = "admin",
                NormalizedEmail = "ADMIN@MAIL.BG",
                NormalizedUserName = "ADMIN"
            };

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");

            users.Add(admin);

            return users;
        }
    }
}
