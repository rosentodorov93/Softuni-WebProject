using FitnessDiary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnesDiary.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("FitnessDiaryInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;

                return new ApplicationDbContext(dbContextOptions, false);
            }
        }
    }
}
