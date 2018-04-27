namespace Domain.Features.Job
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataModel;
    using DataModel.Entities;
    using FluentValidation;
    using MediatR;
    using Pipeline;

    public abstract class EntityExists<T, TEntity> : AbstractValidator<T>
        where TEntity : class, IEntity
    {
        readonly SchedulingDbContext db;

        protected EntityExists(
            Expression<Func<T, Guid>> selector,
            SchedulingDbContext db)
        {
            this.db = db;

            RuleFor(selector)
                .Must(guid => { return db.Set<TEntity>().Any(y => y.Id == guid); });
        }
    }

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid StationId { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.End)
                    .GreaterThan(x => x.Start);
            }
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult>
        {
            readonly SchedulingDbContext db;

            public Handler(SchedulingDbContext db)
            {
                this.db = db;
            }

            protected override Task<CommandResult> HandleCore(Command request)
            {
                db.Jobs.Add(new Job
                {
                    Id = Guid.NewGuid(),
                    StationId = request.StationId,
                    Start = request.Start,
                    End = request.End,
                    State = JobState.None
                });

                return Task.FromResult(CommandResult.Void);
            }
        }
    }
}