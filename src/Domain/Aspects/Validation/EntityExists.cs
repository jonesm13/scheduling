namespace Domain.Aspects.Validation
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using DataModel.Entities;
    using FluentValidation;

    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> EntityMustExist<T, TProperty, TEntity>(
            this IRuleBuilderInitial<T, TProperty> ruleBuilder,
            DbContext db)
            where TEntity : class, IEntity
        {
            return ruleBuilder
                .Must((message, property) =>
                {
                    Guid? value = property as Guid?;
                    if (!value.HasValue)
                    {
                        throw new Exception(""); // TODO
                    }

                    return db.Set<TEntity>().Any(x => x.Id == value.Value);
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