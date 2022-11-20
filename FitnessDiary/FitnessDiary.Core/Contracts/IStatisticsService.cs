using FitnessDiary.Core.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total(string userId);
    }
}
