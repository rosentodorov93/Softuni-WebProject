using FitnessDiary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IAccountService
    {
        IEnumerable<ActivityLevel> GetActivityLevels();
        IEnumerable<FitnessGoal> GetFitnessGoals();
    }
}
