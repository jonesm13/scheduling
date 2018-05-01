namespace Domain.Features.Template
{
    using System;
    using Aspects.Validation;
    using DataModel;
    using DataModel.Entities;
    using FluentValidation;
    using MediatR;
    using Pipeline;

    public class Delete
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid TemplateId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(SchedulingDbContext db)
            {
                RuleFor(x => x.TemplateId)
                    .EntityMustExist<Command, Guid, Template>(db);
            }
        }
    }
}