namespace Domain.Features.Template.Item
{
    using System;
    using MediatR;
    using Pipeline;

    public class Add
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid TemplateId { get; set; }
        }
    }
}