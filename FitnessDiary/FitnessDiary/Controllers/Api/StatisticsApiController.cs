using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers.Api
{
    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsApiController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService _statisticsService)
        {
            statisticsService = _statisticsService;
        }

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result =  await statisticsService.Total(userId);

            return result;

        }
    }
}
