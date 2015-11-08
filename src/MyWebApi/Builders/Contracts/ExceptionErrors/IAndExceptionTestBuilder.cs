// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for adding AndAlso() method to the expected exception tests.
    /// </summary>
    public interface IAndExceptionTestBuilder : IExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining expected exception tests.
        /// </summary>
        /// <returns>The same exception test builder.</returns>
        IExceptionTestBuilder AndAlso();
    }
}
