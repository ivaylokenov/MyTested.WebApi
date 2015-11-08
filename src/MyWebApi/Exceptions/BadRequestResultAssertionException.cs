// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid bad request result.
    /// </summary>
    public class BadRequestResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BadRequestResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public BadRequestResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
