namespace Domain.Features.Template
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using DataModel.Entities;
    using Infrastructure.EntityFramework;
    using MediatR;
    using Pipeline;

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public string Name { get; set; }
        }

        public class Handler : CommandHandler<Command, CommandResult, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override Task<CommandResult> HandleImpl(Command request)
            {
                Db.Templates.Add(new Template
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name
                });

                return Task.FromResult(CommandResult.Void);
            }
        }
    }
}