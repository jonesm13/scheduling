namespace Infrastructure.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public abstract class QueryHandler<TRequest, TResult, TDbContext> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
        where TDbContext : DbContext
    {
        protected readonly TDbContext Db;

        protected QueryHandler(TDbContext db)
        {
            Db = db;
            Db.Configuration.ProxyCreationEnabled = false;
            Db.Configuration.AutoDetectChangesEnabled = false;
        }

        protected abstract Task<TResult> HandleImpl(TRequest request);

        public async Task<TResult> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            return await HandleImpl(request);
        }

        protected DbQuery<TEntity> SetAsNoTracking<TEntity>()
            where TEntity : class
        {
            return Db.Set<TEntity>().AsNoTracking();
        }
    }
}