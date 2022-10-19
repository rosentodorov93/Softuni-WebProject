using FitnessDiary.Core.Contracts;
using FitnessDiary.Infrastructure.Data;

namespace FitnessDiary.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext context;

        public AccountService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<ActivityLevel> GetActivityLevels()
            => context.ActivityLevels;

        public IEnumerable<FitnessGoal> GetFitnessGoals()
            => context.FitnessGoals;

    }
}
