namespace Domain.Tests
{
    using System;
    using MediatR;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleInjector.Lifestyles;

    public abstract class IntegrationTestBase
    {
        protected Func<IMediator> Mediator => () => DiContainerFactory.Instance.GetInstance<IMediator>();

        IDisposable scope;

        [TestInitialize]
        public void Setup()
        {
            scope = AsyncScopedLifestyle.BeginScope(DiContainerFactory.Instance);
        }

        [TestCleanup]
        public void CleanUp()
        {
            scope?.Dispose();
        }
    }
}