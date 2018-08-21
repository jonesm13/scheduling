namespace Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Results;

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
}