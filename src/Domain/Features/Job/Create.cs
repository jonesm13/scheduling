namespace Domain.Features.Job
{
    using System;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using DataModel;
    using DataModel.Entities;
    using FluentValidation;
    using MediatR;
    using Pipeline;

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
            public Validation(SchedulingDbContext db)
            {
                RuleFor(x => x.StationId)
                    .EntityMustExist<Command, Guid, Station>(command => command.StationId, db);
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