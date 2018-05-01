namespace Domain.Features.Station
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using DataModel;
    using DataModel.Entities;
    using FluentValidation;
    using Infrastructure.EntityFramework;
    using MediatR;
    using Pipeline;

    public class Delete
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid StationId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(SchedulingDbContext db)
            {
                RuleFor(x => x.StationId)
                    .EntityMustExist<Command, Guid, Station>(command => command.StationId, db);
            }
        }

        public class Handler : CommandHandler<Command, CommandResult, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override async Task<CommandResult> HandleImpl(Command request)
            {
                Station theStation = await Db.Stations
                    .SingleAsync(x => x.Id == request.StationId);

                Db.Entry(theStation).State = EntityState.Deleted;

                return CommandResult.Void;
            }
        }
    }
}