namespace MyWebApi.Builders.Contracts.InternalServerErrors
{
    using System;
    using Base;
    using ExceptionErrors;

    /// <summary>
    /// Used for testing internal server error results.
    /// </summary>
    public interface IInternalServerErrorTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests internal server error whether it contains exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        IExceptionTestBuilder WithException();

        /// <summary>
        /// Tests internal server error whether it contains exception with the same type and having the same message as the provided exception.
        /// </summary>
        /// <param name="exception">Expected exception.</param>
        /// <returns>Exception test builder.</returns>
        IBaseTestBuilder WithException(Exception exception);
    }
}
