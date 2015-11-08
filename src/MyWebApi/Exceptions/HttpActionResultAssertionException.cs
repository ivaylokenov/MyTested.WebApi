// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Exceptions
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
}
