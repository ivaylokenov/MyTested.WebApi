namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid internal server error result.
    /// </summary>
    public class InternalServerErrorResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InternalServerErrorResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public InternalServerErrorResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
