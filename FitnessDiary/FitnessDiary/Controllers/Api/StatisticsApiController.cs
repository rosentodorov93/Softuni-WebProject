using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Statistics;
using FitnessDiary.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessDiary.Controllers.Api
{
    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsApiController : Controller
    {
        private readonly IStatisticsService statisticsService;
        private readonly IAccountService accountService;

        public StatisticsApiController(IStatisticsService _statisticsService, IAccountService _accountService)
        {
            statisticsService = _statisticsService;
            accountService = _accountService;
        }

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics()
        {
            var userId = accountService.GetById(this.User.Id());

            var result =  await statisticsService.Total(userId);

            return result;

        }
    }
}
