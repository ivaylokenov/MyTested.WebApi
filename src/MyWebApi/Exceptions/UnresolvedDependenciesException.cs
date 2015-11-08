// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for controller unresolved dependencies.
    /// </summary>
    public class UnresolvedDependenciesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnresolvedDependenciesException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public UnresolvedDependenciesException(string message)
            : base(message)
        {
        }
    }
}
