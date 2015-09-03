namespace MyWebApi.Builders.ExceptionErrors
{
    using System.Net;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ExceptionErrors;
    using Exceptions;

    public class HttpResponseExceptionTestBuilder : BaseTestBuilder, IHttpResponseExceptionTestBuilder
    {
        private readonly HttpResponseException httpResponseException;

        public HttpResponseExceptionTestBuilder(
            ApiController controller,
            string actionName,
            HttpResponseException caughtException)
            : base(controller, actionName, caughtException)
        {
            this.httpResponseException = caughtException;
        }

        public IBaseTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            var actualStatusCode = this.httpResponseException.Response.StatusCode;
            if (actualStatusCode != statusCode)
            {
                throw new HttpStatusCodeAssertionException(string.Format(
                    "When calling {0} action in {1} expected HttpResponseException to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetName(),
                    (int)statusCode,
                    statusCode,
                    (int)actualStatusCode,
                    actualStatusCode));
            }

            return this.NewAndProvideTestBuilder();
        }
    }
}
