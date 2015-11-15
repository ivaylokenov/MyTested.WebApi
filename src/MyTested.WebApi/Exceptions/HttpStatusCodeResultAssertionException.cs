// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid status code result when expecting certain HTTP status code.
    /// </summary>
    public class HttpStatusCodeResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpStatusCodeResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpStatusCodeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
