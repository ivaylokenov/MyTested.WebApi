namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing expected exceptions.
    /// </summary>
    public interface IExceptionTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether certain type of exception is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TException">Type of the expected exception.</typeparam>
        /// <returns>The same exception test builder.</returns>
        IExceptionTestBuilder OfType<TException>();

        /// <summary>
        /// Tests exception message using test builder.
        /// </summary>
        /// <returns>Exception message test builder.</returns>
        IExceptionMessageTestBuilder WithMessage();

        /// <summary>
        /// Tests exception message whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected exception message as string.</param>
        /// <returns>The same exception test builder.</returns>
        IExceptionTestBuilder WithMessage(string message);

        /// <summary>
        /// AndAlso method for better readability when chaining expected exception tests.
        /// </summary>
        /// <returns>The same exception test builder.</returns>
        IExceptionTestBuilder AndAlso();
    }
}
