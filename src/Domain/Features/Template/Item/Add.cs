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
    using Helpers;
    using Infrastructure.EntityFramework;
    using MediatR;
    using Pipeline;

    public class Add
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid TemplateId { get; set; }
            public int? Order { get; set; }
            public TemplateItemType Type { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(SchedulingDbContext db)
            {
                RuleFor(x => x.TemplateId)
                    .EntityMustExist<Command, Guid, Template>(db);

                RuleFor(x => x.Order)
                    .Must(BeValidOrdinalPosition);
            }

            bool BeValidOrdinalPosition(int? arg)
            {
                return !arg.HasValue || arg.Value >= 0;
            }
        }

        public class Handler : CommandHandler<Command, CommandResult, SchedulingDbContext>
        {
            public Handler(SchedulingDbContext db) : base(db)
            {
            }

            protected override async Task<CommandResult> HandleImpl(Command request)
            {
                Template theTemplate = await Db.Templates
                    .Include(x => x.Items)
                    .SingleAsync(x => x.Id == request.TemplateId);

                TemplateItem item = new TemplateItem
                {
                    Id = SequentualGuid.New(),
                    State = string.Empty,
                    TemplateId = theTemplate.Id,
                    Type = request.Type,
                    Order = GetOrder(request.Order, theTemplate)
                };

                theTemplate.Items.Add(item);

                return CommandResult.Void;
            }

            static int GetOrder(int? requestedOrder, Template theTemplate)
            {
                if (!theTemplate.Items.Any())
                {
                    return 0;
                }

                int maxPosition = theTemplate
                    .Items
                    .OrderByDescending(x => x.Order)
                    .First()
                    .Order;

                if (!requestedOrder.HasValue || requestedOrder > maxPosition)
                {
                    return maxPosition + 1;
                }

                return 0;
            }
        }
    }
}