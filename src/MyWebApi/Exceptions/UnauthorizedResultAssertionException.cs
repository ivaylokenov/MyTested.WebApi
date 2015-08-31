namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid unauthorized result when authentication header challenges do not match.
    /// </summary>
    public class UnauthorizedResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnauthorizedResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public UnauthorizedResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
