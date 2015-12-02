// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpResponseMessages
{
    using System;

    /// <summary>
    /// Used for testing HTTP response message with response time measurements.
    /// </summary>
    public interface IHttpHandlerResponseMessageWithTimeTestBuilder : IHttpHandlerResponseMessageTestBuilder
    {
        /// <summary>
        /// Tests whether the measured response time passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions);

        /// <summary>
        /// Tests whether the measured response time passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate);

        /// <summary>
        /// Gets the response time measured in the testing.
        /// </summary>
        /// <returns>Instance of TimeSpan.</returns>
        TimeSpan AndProvideTheResponseTime();
    }
}
