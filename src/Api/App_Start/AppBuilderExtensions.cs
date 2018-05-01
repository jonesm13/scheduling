namespace Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Results;
    using FluentValidation;
    using FluentValidation.Results;
    using IoC;
    using log4net.Config;
    using MediatR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Owin;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using ApiController = Infrastructure.WebApi.ApiController;

    // ReSharper disable once InconsistentNaming
    public static class IAppBuilderExtensions
    {
        static IEnumerable<Assembly> DomainAssembly
        {
            get
            {
                yield return Assembly.Load("Domain");
            }
        }

        static IEnumerable<Assembly> ProcessAssembly
        {
            get
            {
                yield return Assembly.GetExecutingAssembly();
            }
        }

        public static IAppBuilder UseLog4Net(this IAppBuilder app)
        {
            XmlConfigurator.Configure();

            return app;
        }

        public static IAppBuilder UseWebApi(this IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            Container container = ContainerFactory.Create(
                new AsyncScopedLifestyle(),
                DomainAssembly.ToList(),
                c =>
                {
                    c.RegisterWebApiControllers(config);

                    c.RegisterInitializer<ApiController>(
                        apiController =>
                        {
                            apiController.Mediator = c.GetInstance<IMediator>();
                        });

                    IEnumerable<Type> notificationHandlers =
                        c.GetTypesToRegister(
                            typeof(INotificationHandler<>),
                            ProcessAssembly,
                            new TypesToRegisterOptions
                            {
                                IncludeGenericTypeDefinitions = true
                            });

                    c.RegisterCollection(
                        typeof(INotificationHandler<>),
                        notificationHandlers);
                });

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            // formatters
            config.Formatters.Clear();
            JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SupportedMediaTypes.Clear();
            jsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("application/json"));
            config.Formatters.Add(jsonFormatter);

            // set JSON serialiser used by WebApi to use our desired serialisation settings
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver =
                        new CamelCasePropertyNamesContractResolver(),
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateParseHandling = DateParseHandling.DateTime,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore
                };

            config.Services.Replace(
                typeof(IExceptionHandler),
                new CustomExceptionHandler());

            app.UseWebApi(config);

            return app;
        }
    }

    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            switch (context.Exception)
            {
                case ValidationException exception:
                    string message = "Validation failed.";
                    string errorCode;

                    List<ValidationFailure> errors = exception.Errors.ToList();

                    if (errors.Count > 1)
                    {
                        errorCode = errors.Select(e => e.ErrorCode)
                            .GroupBy(e => e)
                            .OrderByDescending(g => g.Count())
                            .First()
                            .Key;
                    }
                    else
                    {
                        message = errors.First()
                            .ErrorMessage;
                        errorCode = errors.First()
                            .ErrorCode;
                    }

                    if (!Enum.TryParse(
                        errorCode,
                        out HttpStatusCode httpStatusCode))
                        httpStatusCode = (HttpStatusCode) 422;

                    context.CreateResponse(
                        httpStatusCode,
                        new FailureResult
                        {
                            Message = message,
                            Errors = errors.Select(
                                e => new
                                {
                                    e.PropertyName,
                                    e.AttemptedValue,
                                    e.ErrorMessage
                                })
                        });
                    break;

                default:
                    context.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new FailureResult
                        {
                            Message = context.Exception.Message,
                            Errors = context.Exception.GetInnerExceptions()
                                .Select(ex => ex.Message)
                        });
                    break;
            }
        }
    }

    public class FailureResult
    {
        public string Message { get; set; }
        public object Errors { get; set; }
    }

    public static class ExceptionHandlerExtensions
    {
        public static void CreateResponse<T>(
            this ExceptionHandlerContext context,
            HttpStatusCode httpStatusCode,
            T value)
        {
            context.Result = new ResponseMessageResult(
                context.Request.CreateResponse(httpStatusCode, value));
        }
    }

    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Recursively unpacks an <see cref="Exception" />, yielding the
        ///     exception itself as well as every nested
        ///     <see cref="Exception.InnerException" />.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetExceptions(
            this Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Exception ex = exception;

            do
            {
                yield return ex;
                ex = ex.InnerException;
            }
            while (ex != null);
        }

        /// <summary>
        ///     Recursively unpacks an <see cref="Exception" />, yielding
        ///     every nested <see cref="Exception.InnerException" />.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetInnerExceptions(
            this Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Exception ex = exception.InnerException;

            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}