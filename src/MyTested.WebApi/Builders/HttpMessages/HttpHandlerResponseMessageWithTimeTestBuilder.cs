// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using Contracts.HttpResponseMessages;

    /// <summary>
    /// Used for testing HTTP response message with response time measurements.
    /// </summary>
    public class HttpHandlerResponseMessageWithTimeTestBuilder
        : HttpHandlerResponseMessageTestBuilder, IAndHttpHandlerResponseMessageWithTimeTestBuilder
    {
        private readonly TimeSpan responseTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerResponseMessageWithTimeTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">Tested HTTP message handler.</param>
        /// <param name="httpResponseMessage">HTTP response result from the tested handler.</param>
        /// <param name="responseTime">Measured response time from the tested handler.</param>
        public HttpHandlerResponseMessageWithTimeTestBuilder(
            HttpMessageHandler handler,
            HttpResponseMessage httpResponseMessage,
            TimeSpan responseTime)
            : base(handler, httpResponseMessage)
        {
            this.responseTime = responseTime;
        }

        /// <summary>
        /// Tests whether the measured response time passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions)
        {
            assertions(this.responseTime);
            return this;
        }

        /// <summary>
        /// Tests whether the measured response time passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate)
        {
            if (!predicate(this.responseTime))
            {
                this.ThrowNewHttpResponseMessageAssertionException("response time", "to pass the given condition", "it failed");
            }

            return this;
        }

        /// <summary>
        /// Gets the response time measured in the testing.
        /// </summary>
        /// <returns>Instance of TimeSpan.</returns>
        public TimeSpan AndProvideTheResponseTime()
        {
            return this.responseTime;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message with response time tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public new IHttpHandlerResponseMessageWithTimeTestBuilder AndAlso()
        {
            return this;
        }
    }
}
