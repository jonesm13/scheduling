namespace Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http.ExceptionHandling;
    using FluentValidation;
    using FluentValidation.Results;
    using Helpers;

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
                        message = errors.First().ErrorMessage;
                        errorCode = errors.First().ErrorCode;
                    }

                    if (!Enum.TryParse(
                        errorCode,
                        out HttpStatusCode httpStatusCode))
                    {
                        httpStatusCode = (HttpStatusCode)422;
                    }

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
                            Errors = context
                                .Exception
                                .GetInnerExceptions()
                                .Select(ex => ex.Message)
                        });
                    break;
            }
        }
    }
}