// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi
{
    using System;
    using System.Net.Http;
    using Builders.Contracts.HttpRequests;
    using Builders.Contracts.Servers;

    /// <summary>
    /// Provides options to set the HTTP request to test.
    /// </summary>
    public interface IServerBuilder
    {
        /// <summary>
        /// Adds HTTP request message to the tested server.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        IServerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage);

        /// <summary>
        /// Adds HTTP request message to the tested server.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        IServerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder);
    }
}
