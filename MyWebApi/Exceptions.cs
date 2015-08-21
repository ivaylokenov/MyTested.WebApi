namespace MyWebApi
{
    using System;

    /// <summary>
    /// Exception for invalid action return type when expecting IHttpActionResult.
    /// </summary>
    public class IHttpActionResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the IHttpActionResultAssertionException class.
        /// </summary>
        /// <param name="message">Message required by System.Exception class.</param>
        public IHttpActionResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
