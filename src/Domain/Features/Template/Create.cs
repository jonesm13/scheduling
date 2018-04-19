namespace Domain.Features.Template
{
    using System.Threading.Tasks;
    using MediatR;
    using Pipeline;

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public string Name { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, CommandResult>
        {
            protected override Task<CommandResult> HandleCore(Command request)
            {
                return Task.FromResult(CommandResult.Void);
            }
        }
    }
}