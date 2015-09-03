namespace MyWebApi.Builders.ExceptionErrors
{
    using System.Net;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ExceptionErrors;
    using Exceptions;

    /// <summary>
    /// Used for testing expected HttpResponseException.
    /// </summary>
    public class HttpResponseExceptionTestBuilder : BaseTestBuilder, IHttpResponseExceptionTestBuilder
    {
        private readonly HttpResponseException httpResponseException;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseExceptionTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Actual received HttpResponseException.</param>
        public HttpResponseExceptionTestBuilder(
            ApiController controller,
            string actionName,
            HttpResponseException caughtException)
            : base(controller, actionName, caughtException)
        {
            this.httpResponseException = caughtException;
        }

        /// <summary>
        /// Tests whether caught HttpResponseException has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder.</returns>
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
