// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.ExceptionErrors
{
    using System;

    /// <summary>
    /// Used for testing AggregateException.
    /// </summary>
    public interface IAggregateExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether AggregateException contains inner exception of the provided type.
        /// </summary>
        /// <typeparam name="TInnerException">Expected inner exception type.</typeparam>
        /// <returns>The same aggregate exception test builder.</returns>
        IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception;
    }
}
