namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.ExceptionErrors;

    public class HttpResponseExceptionTestBuilder : BaseTestBuilder, IHttpResponseExceptionTestBuilder
    {
        public HttpResponseExceptionTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }
    }
}
