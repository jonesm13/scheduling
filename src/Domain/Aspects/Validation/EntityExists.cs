namespace Domain.Aspects.Validation
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using DataModel.Entities;
    using FluentValidation;

    public abstract class EntityExists<T, TEntity> : AbstractValidator<T>
        where TEntity : class, IEntity
    {
        protected EntityExists(
            Expression<Func<T, Guid>> selector,
            DbContext db)
        {
            RuleFor(selector)
                .Must(guid => db.Set<TEntity>().Any(y => y.Id == guid))
                .WithHttpStatusCode(HttpStatusCode.NotFound);
        }
    }

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithHttpStatusCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            HttpStatusCode code)
        {
            return ruleBuilder.WithErrorCode(((int)code).ToString());
        }
    }
}