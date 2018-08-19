namespace Domain.Features.Template.Item
{
    using System;
    using System.Data.Entity;
    using System.Linq;
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
            public Guid TemplateId { get; set; }
            public int Order { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(SchedulingDbContext db)
            {
                RuleFor(x => x.TemplateId)
                    .EntityMustExist<Command, Guid, Template>(db);
            }
        }

        public class Handler : CommandHandler<Command, CommandResult, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override async Task<CommandResult> HandleImpl(Command request)
            {
                Template template = await Db.Templates
                    .Include(x => x.Items)
                    .SingleAsync(x => x.Id == request.TemplateId);

                TemplateItem toDelete = template
                    .Items
                    .First(x => x.Order == request.Order);

                Db.Entry(toDelete).State = EntityState.Deleted;

                return CommandResult.Void;
            }
        }
    }
}