namespace Domain.Aspects.Validation
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using DataModel.Entities;
    using FluentValidation;

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, TProperty> EntityMustExist<T, TProperty, TEntity>(
            this IRuleBuilderInitial<T, TProperty> ruleBuilder,
            Expression<Func<T, Guid>> selector,
            DbContext db)
            where TEntity : class, IEntity
        {
            return ruleBuilder
                .Must((message, property) =>
                {
                    Guid id = selector.Compile().Invoke(message);
                    return db.Set<TEntity>().Any(x => x.Id == id);
                })
                .WithHttpStatusCode(HttpStatusCode.NotFound);
        }

        public static IRuleBuilderOptions<T, TProperty> WithHttpStatusCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            HttpStatusCode code)
        {
            return ruleBuilder.WithErrorCode(((int)code).ToString());
        }
    }
}