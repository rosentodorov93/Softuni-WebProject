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
            builder.HasData(CreateUsers());
        }

        private List<IdentityUser> CreateUsers()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                Id = "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.bg",
                NormalizedEmail = "ADMIN@MAIL.BG"
            };

            user.PasswordHash = hasher.HashPassword(user, "admin123");

            users.Add(user);

            var user2 = new IdentityUser()
            {
                Id = "02b52032-ec58-496e-b58e-0533479ff27d",
                UserName = "moderator",
                NormalizedUserName = "MODERATOR",
                Email = "moderator@mail.bg",
                NormalizedEmail = "MODERATOR@MAIL.BG"
            };

            user2.PasswordHash = hasher.HashPassword(user2, "mod123");

            users.Add(user2);

            return users;
        }
    }
}
