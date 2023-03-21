namespace WarzoneFanout.Application.Query.QueryHandlers
{
    public interface IQueryHandler<in T, TResult>
    {
        Task<TResult> HandleAsync(T query, CancellationToken ct);
    }
}
