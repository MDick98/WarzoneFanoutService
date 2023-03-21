using WarzoneFanout.Application.Query.Queries;
using WarzoneFanout.Application.Query.Results;

namespace WarzoneFanout.Application.Query.QueryHandlers
{
    public class GetStatsByUsernameQueryHandler : IQueryHandler<GetStatsByUsernameQuery, StatsResult>
    {
        public async Task<StatsResult> HandleAsync(GetStatsByUsernameQuery query, CancellationToken ct)
        {
            //get to repository

            if (query.GameType == Domain.GameType.Multiplayer)
            {
                return new StatsResult(query.GameType.ToString(), 8514, 562, 695148, 143);
            }
            return new StatsResult(query.GameType.ToString(), 851, 566, 69514, 351);
        }
    }
}
