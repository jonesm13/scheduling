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

            // handlers
            result.Register(typeof(IRequestHandler<,>), assemblies);
            result.Register(typeof(INotificationHandler<>), assemblies);

            result.RegisterCollection(typeof(IRequestPreProcessor<>), assemblies);

            // pipelines
            result.RegisterCollection(typeof(IPipelineBehavior<,>), assemblies);

            // processors
            result.RegisterDecorator(typeof(IRequestHandler<,>), typeof(HandlerDecorator<,>));

            result.RegisterCollection(typeof(IValidator<>), assemblies);

            // factories
            result.RegisterInstance(new SingleInstanceFactory(result.GetInstance));
            result.RegisterInstance(new MultiInstanceFactory(result.GetAllInstances));

            customConfig?.Invoke(result);

            result.Verify();

            return result;
        }
    }
}
