// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for adding AndAlso() method to the aggregate exception tests.
    /// </summary>
    public interface IAndAggregateExceptionTestBuilder : IAggregateExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining aggregate exception tests.
        /// </summary>
        /// <returns>The same aggregate exception test builder.</returns>
        IAggregateExceptionTestBuilder AndAlso();
    }
}
