using Grpc.Net.Client;
using MediatR;
using WarzoneFanout.Application.Requests;
using WarzoneFanout.Application.Responses;
using WarzoneFanout.Domain;

namespace WarzoneFanout.Application.Handlers
{
    public class GetStatsHandler : IRequestHandler<GetStatsRequest, StatsResponse>
    {
        private readonly List<GameType> gameTypes = new()
        {
            GameType.Multiplayer,
            GameType.BattleRoyal
        };
        public async Task<StatsResponse> Handle(GetStatsRequest request, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7028");
            var client = new Infastructure.Stats.StatsClient(channel);

            List<GameStats> gameStats = new();

            try
            {
                foreach (var gameType in gameTypes)
                {
                    var reply = await client.GetStatsAsync(
                                  new Infastructure.StatsRequest { Username = request.Username, GameType = (Infastructure.GameType)gameType }, cancellationToken:cancellationToken);

                    gameStats.Add(new GameStats
                    {
                        GameType = gameType.ToString(),
                        TotalKills = reply.TotalKills,
                        TotalDeaths = reply.TotalDeaths,
                        Score = reply.Score,
                        MatchesPlayed = reply.MatchesPlayed
                    });
                }
            }
            catch (Exception ex)
            {
                return new StatsResponse
                {
                    AllStats = new AllStats
                    {
                        ErrorMessage = ex.Message,
                        Username = request.Username
                    }
                };
            }

            return new StatsResponse
            {
                AllStats = new AllStats
                {
                    GameStats = gameStats,
                    Username = request.Username
                }
            };
            
        }
    }
}
