using WarzoneFanout.Application.Query.Queries;
using WarzoneFanout.Application.Query.QueryHandlers;
using WarzoneFanout.Application.Query.Results;
using WarzoneFanout.Grpc.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IQueryHandler<GetStatsByUsernameQuery, StatsResult>, GetStatsByUsernameQueryHandler>();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<StatsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
