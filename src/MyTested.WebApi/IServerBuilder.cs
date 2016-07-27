// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
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
        /// Adds canceled cancellation token to the server request.
        /// </summary>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithCancellationToken();

        /// <summary>
        /// Adds cancellation token to the server request.
        /// </summary>
        /// <param name="cancelled">True or false indicating whether the token is canceled.</param>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithCancellationToken(bool cancelled);

        /// <summary>
        /// Adds cancellation token to the server request.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to add to the server request.</param>
        /// <returns>The same server builder.</returns>
        IServerBuilder WithCancellationToken(CancellationToken cancellationToken);

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
