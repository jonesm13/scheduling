namespace Api.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using DataModel;
    using Domain.Pipeline;
    using FluentValidation;
    using MediatR;
    using MediatR.Pipeline;
    using SimpleInjector;

    public class ContainerFactory
    {
        public static Container Create(
            ScopedLifestyle scopedLifestyle,
            ICollection<Assembly> assemblies,
            Action<Container> customConfig)
        {
            Container result = new Container();

            result.Options.DefaultScopedLifestyle = scopedLifestyle;

            // data context
            result.Register(SchedulingDbContext.Create, Lifestyle.Scoped);

            // MediatR
            result.RegisterSingleton<IMediator, Mediator>();

            result.Register(() => new ServiceFactory(result.GetInstance), Lifestyle.Singleton);

            // handlers
            result.Register(typeof(IRequestHandler<,>), assemblies);
            result.Register(typeof(INotificationHandler<>), assemblies);

            result.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);

            // pipelines
            result.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);

            // processors
            result.RegisterDecorator(typeof(IRequestHandler<,>), typeof(HandlerDecorator<,>));

            result.Collection.Register(typeof(IValidator<>), assemblies);

            customConfig?.Invoke(result);

            result.Verify();

            return result;
        }
    }
}
