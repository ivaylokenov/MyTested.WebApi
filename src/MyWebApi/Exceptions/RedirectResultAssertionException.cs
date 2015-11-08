// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid URI validation.
    /// </summary>
    public class RedirectResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RedirectResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public RedirectResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
