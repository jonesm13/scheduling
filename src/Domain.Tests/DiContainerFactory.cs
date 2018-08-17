namespace Domain.Tests
{
    using System;
    using System.Reflection;
    using DataModel;
    using FluentValidation;
    using MediatR;
    using MediatR.Pipeline;
    using Pipeline;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class DiContainerFactory
    {
        static readonly Lazy<Container> ContainerLoader = new Lazy<Container>(LoadContainer);

        static Container LoadContainer()
        {
            Container container = new Container();

            Assembly[] assemblies = { typeof(IDomainLivesHere).Assembly };

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // data context
            container.Register(SchedulingDbContext.Create, Lifestyle.Scoped);

            // mediator
            container.RegisterSingleton<IMediator, Mediator>();

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            // decorator
            container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(HandlerDecorator<,>));

            // handlers
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);

            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);

            // pipelines
            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);

            // validators
            container.Collection.Register(typeof(IValidator<>), assemblies);

            return container;
        }

        public static Container Instance => ContainerLoader.Value;
    }
}