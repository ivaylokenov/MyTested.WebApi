namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for response model with errors.
    /// </summary>
    public class ResponseModelErrorAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ResponseModelErrorAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ResponseModelErrorAssertionException(string message)
            : base(message)
        {
        }
    }
}
