namespace MyWebApi
{
    using System;

    /// <summary>
    /// Exception for invalid action return type when expecting IHttpActionResult.
    /// </summary>
    public class HttpActionResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpActionResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpActionResultAssertionException(string message)
            : base(message)
        {
        }
    }

    public class ResponseModelAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ResponseModelAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ResponseModelAssertionException(string message)
            : base(message)
        {
        }
    }
}
