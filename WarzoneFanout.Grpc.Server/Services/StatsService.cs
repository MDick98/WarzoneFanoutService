using Grpc.Core;
using WarzoneFanout.Application.Query.Queries;
using WarzoneFanout.Application.Query.QueryHandlers;
using WarzoneFanout.Application.Query.Results;
using WarzoneFanout.Grpc.Server;

namespace WarzoneFanout.Grpc.Server.Services
{
    public class StatsService : Stats.StatsBase
    {
        private readonly ILogger<StatsService> _logger;
        private readonly IQueryHandler<GetStatsByUsernameQuery, StatsResult> queryHandler;
        public StatsService(ILogger<StatsService> logger, IQueryHandler<GetStatsByUsernameQuery, StatsResult> queryHandler)
        {
            _logger = logger;
            this.queryHandler = queryHandler;
        }

        public override async Task<StatsResponse> GetStats(StatsRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Retrieving stats for Username: {Username}", request.Username);

            var result = await queryHandler.HandleAsync(new GetStatsByUsernameQuery(request.Username, (Domain.GameType)request.GameType), context.CancellationToken).ConfigureAwait(false);

            return new StatsResponse
            {
                TotalKills = result.TotalKills,
                TotalDeaths = result.TotalDeaths,
                Score = result.Score,
                MatchesPlayed = result.MatchesPlayed
            };
        }
    }
}