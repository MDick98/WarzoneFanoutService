using MediatR;
using WarzoneFanout.Application.Responses;

namespace WarzoneFanout.Application.Requests
{
    public class GetStatsRequest : IRequest<StatsResponse>
    {
        public string Username { get; set; }
    }
}
