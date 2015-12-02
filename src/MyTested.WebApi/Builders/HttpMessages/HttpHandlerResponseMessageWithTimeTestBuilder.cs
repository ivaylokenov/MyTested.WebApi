// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using Contracts.HttpResponseMessages;

    public class HttpHandlerResponseMessageWithTimeTestBuilder
        : HttpHandlerResponseMessageTestBuilder, IAndHttpHandlerResponseMessageWithTimeTestBuilder
    {
        private readonly TimeSpan responseTime;

        public HttpHandlerResponseMessageWithTimeTestBuilder(
            HttpMessageHandler handler,
            HttpResponseMessage httpResponseMessage,
            TimeSpan responseTime)
            : base(handler, httpResponseMessage)
        {
            this.responseTime = responseTime;
        }

        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions)
        {
            assertions(this.responseTime);
            return this;
        }

        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate)
        {
            if (!predicate(this.responseTime))
            {
                this.ThrowNewHttpResponseMessageAssertionException("response time", "pass the given condition", "it failed");
            }

            return this;
        }

        public TimeSpan AndProvideTheResponseTime()
        {
            return this.responseTime;
        }

        public IHttpHandlerResponseMessageWithTimeTestBuilder AndAlso()
        {
            return this;
        }
    }
}
