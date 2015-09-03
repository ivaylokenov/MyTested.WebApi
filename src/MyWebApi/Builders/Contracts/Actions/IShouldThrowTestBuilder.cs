namespace MyWebApi.Builders.Contracts.Actions
{
    using Base;
    using ExceptionErrors;

    /// <summary>
    /// Used for testing whether action throws exception.
    /// </summary>
    public interface IShouldThrowTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether action throws any exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        IExceptionTestBuilder Exception();

        /// <summary>
        /// Tests whether action throws any HttpResponseException.
        /// </summary>
        /// <returns>HttpResponseException test builder.</returns>
        IHttpResponseExceptionTestBuilder HttpResponseException();
    }
}
