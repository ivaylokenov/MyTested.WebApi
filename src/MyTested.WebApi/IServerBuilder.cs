// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Builders.Contracts.HttpRequests;
    using Builders.Contracts.Servers;

    /// <summary>
    /// Provides options to set the HTTP request to test.
    /// </summary>
    public interface IServerBuilder
    {
        /// <summary>
        /// Adds default header to every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithDefaultRequestHeader(string name, string value);

        /// <summary>
        /// Adds default header to every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithDefaultRequestHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds default collection of headers to every request tested on the server.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithDefaultRequestHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Removes a previously added default header from every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <returns></returns>
        IServerBuilder WithoutDefaultRequestHeader(string name);

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
