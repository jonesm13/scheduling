namespace Domain.Features.Job
{
    using System;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using DataModel;
    using DataModel.Entities;
    using FluentValidation;
    using Helpers;
    using Infrastructure.EntityFramework;
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
                    .EntityMustExist<Command, Guid, Station>(db);

                RuleFor(x => x.Start)
                    .Must((command, time) => time < command.End);
            }
        }

        public class Handler : CommandHandler<Command, CommandResult, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override Task<CommandResult> HandleImpl(Command request)
            {
                Guid jobId = SequentualGuid.New();

                Db.Jobs.Add(new Job
                {
                    Id = jobId,
                    StationId = request.StationId,
                    Start = request.Start,
                    End = request.End,
                    State = JobState.None
                });

                return Task.FromResult(CommandResult.Void.WithNotification(new JobCreated { JobId = jobId }));
            }
        }

        public class JobCreated : INotification
        {
            public Guid JobId { get; set; }
        }
    }
}