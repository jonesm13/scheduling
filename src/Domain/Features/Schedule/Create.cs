namespace Domain.Features.Schedule
{
    using System;
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
            public Guid TemplateId { get; set; }
            public DayOfTheWeek[] Days { get; set; }
            public int StartTime { get; set; }
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

    public enum DayOfTheWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}