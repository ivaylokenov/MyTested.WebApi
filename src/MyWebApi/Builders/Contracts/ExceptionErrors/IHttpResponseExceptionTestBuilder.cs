namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    using System.Net;
    using Base;

    /// <summary>
    /// Used for testing expected HttpResponseException.
    /// </summary>
    public interface IHttpResponseExceptionTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether caught HttpResponseException has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
