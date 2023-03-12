using Grpc.Core;
using WarzoneFanout.Grpc.Server;

namespace WarzoneFanout.Grpc.Server.Services
{
    public class StatsService : Stats.StatsBase
    {
        private readonly ILogger<StatsService> _logger;
        public StatsService(ILogger<StatsService> logger)
        {
            _logger = logger;
        }

        public override Task<StatsResponse> GetStats(StatsRequest request, ServerCallContext context)
        {

            //Add request to repository to retrieve data
            if (request.GameType == GameType.Multiplayer)
            {
                return Task.FromResult(new StatsResponse
                {
                    TotalKills = 1000,
                    TotalDeaths = 800,
                    Score = 134500,
                    MatchesPlayed = 270
                });
            }

            return Task.FromResult(new StatsResponse
            {
                TotalKills = 100,
                TotalDeaths = 80,
                Score = 13450,
                MatchesPlayed = 27
            });
        }
    }
}