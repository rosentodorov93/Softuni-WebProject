using FitnessDiary.Infrastructure.Data.Account;
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
    internal class AdministrationUserConfiguration : IEntityTypeConfiguration<AdministrationUser>
    {
        public void Configure(EntityTypeBuilder<AdministrationUser> builder)
        {
            builder.HasData(CreateAdministrationUsers());
        }

        private List<AdministrationUser> CreateAdministrationUsers()
        {
            var users = new List<AdministrationUser>();


            var admin = new AdministrationUser()
            {
                FirstName = "Admin",
                LastName = "Adminov",
                Id = "0c68b5c9-40f7-44de-b03b-0afe35157e35",
                UserId = "cf28b02f-bcd9-4464-9100-6343cc8ca939",
            };
           
            admin.UserId = "cf28b02f-bcd9-4464-9100-6343cc8ca939";

            users.Add(admin);

            var moderator = new AdministrationUser()
            {
                FirstName = "Moderator",
                LastName = "Moderatorov",
                Id = "066f7491-cf3e-481a-9afd-9a4e8e276a50",
                UserId = "02b52032-ec58-496e-b58e-0533479ff27d",
            };
           
            users.Add(moderator);

            return users;
        }
    }
}
