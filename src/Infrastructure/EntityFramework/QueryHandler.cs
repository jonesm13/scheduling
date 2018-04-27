namespace Infrastructure.EntityFramework
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using MediatR;

    public abstract class QueryHandler<TRequest, TResult, TDbContext> : AsyncRequestHandler<TRequest, TResult>
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

        protected DbQuery<TEntity> SetAsNoTracking<TEntity>()
            where TEntity : class
        {
            return Db.Set<TEntity>().AsNoTracking();
        }
    }
}