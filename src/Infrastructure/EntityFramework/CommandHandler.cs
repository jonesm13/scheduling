namespace Infrastructure.EntityFramework
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using MediatR;

    public abstract class CommandHandler<TRequest, TResponse, TDbContext> : AsyncRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TDbContext : DbContext
    {
        protected readonly TDbContext Db;

        protected CommandHandler(TDbContext db)
        {
            Db = db;
        }

        protected abstract Task<TResponse> HandleImpl(TRequest request);

        protected override async Task<TResponse> HandleCore(TRequest request)
        {
            TResponse response = await HandleImpl(request);

            await Db.SaveChangesAsync();

            return response;
        }
    }
}