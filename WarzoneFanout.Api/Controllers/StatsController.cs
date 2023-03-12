using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using WarzoneFanout.Domain;
using MediatR;
using WarzoneFanout.Application.Requests;

namespace WarzoneFanout.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> logger;
        private readonly IMediator mediator;

        public StatsController(ILogger<StatsController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("{username}")]
        public async Task<AllStats> GetAsync(string username)
        {
            var repsonse = await mediator.Send(new GetStatsRequest { Username = username }).ConfigureAwait(false);
            return repsonse.AllStats;
        }
    }
}