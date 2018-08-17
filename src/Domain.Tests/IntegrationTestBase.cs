namespace Domain.Tests
{
    using System;
    using MediatR;

    public abstract class IntegrationTestBase
    {
        protected Func<IMediator> Mediator => () => DiContainerFactory.Instance.GetInstance<IMediator>();
    }
}