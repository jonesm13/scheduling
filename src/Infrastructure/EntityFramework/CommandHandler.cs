namespace Infrastructure.EntityFramework
{
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public abstract class CommandHandler<TRequest, TResponse, TDbContext> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TDbContext : DbContext
    {
        protected readonly TDbContext Db;

        protected CommandHandler(TDbContext db)
        {
            Db = db;
        }

        protected abstract Task<TResponse> HandleImpl(
            TRequest request);

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            TResponse response = await HandleImpl(request);

            await Db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}