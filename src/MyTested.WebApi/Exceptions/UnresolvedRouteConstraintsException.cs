namespace MyTested.WebApi.Exceptions
{
    using System;
    public class UnresolvedRouteConstraintsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnresolvedRouteConstraintsException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public UnresolvedRouteConstraintsException(string message)
            : base(message)
        {
        }
    }
}
