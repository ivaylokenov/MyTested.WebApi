// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid content result.
    /// </summary>
    public class ContentResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContentResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ContentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
