// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Servers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using Common.Extensions;
    using Common.Servers;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;
    using Contracts.Servers;
    using HttpMessages;

    /// <summary>
    /// Provides options to set the HTTP request and test the HTTP response.
    /// </summary>
    public class ServerTestBuilder : IServerBuilder, IServerTestBuilder
    {
        private readonly HttpClient client;
        private readonly bool transformRequest;
        private readonly bool disposeServer;
        private readonly IDisposable server;

        private HttpRequestMessage httpRequestMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerTestBuilder" /> class.
        /// </summary>
        /// <param name="client">HTTP message invoker to send the request.</param>
        /// <param name="transformRequest">Indicates whether to transform relative to fake absolute request URI.</param>
        /// <param name="disposeServer">Indicates whether to dispose the server and the client after the test completes.</param>
        /// <param name="server">IDisposable server to use for the request.</param>
        public ServerTestBuilder(
            HttpClient client,
            bool transformRequest = false,
            bool disposeServer = false,
            IDisposable server = null)
        {
            this.client = client;
            this.transformRequest = transformRequest;
            this.disposeServer = disposeServer;
            this.server = server;
        }

        /// <summary>
        /// Adds default header to every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same server builder.</returns>
        public IServerBuilder WithDefaultRequestHeader(string name, string value)
        {
            this.client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds default header to every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same server builder.</returns>
        public IServerBuilder WithDefaultRequestHeader(string name, IEnumerable<string> values)
        {
            this.client.DefaultRequestHeaders.Add(name, values);
            return this;
        }

        /// <summary>
        /// Adds default collection of headers to every request tested on the server.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same server builder.</returns>
        public IServerBuilder WithDefaultRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithDefaultRequestHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Removes a previously added default header from every request tested on the server.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <returns></returns>
        public IServerBuilder WithoutDefaultRequestHeader(string name)
        {
            if (client.DefaultRequestHeaders.Contains(name))
            {
                client.DefaultRequestHeaders.Remove(name);
            }

            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested server.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        public IServerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            if (this.transformRequest)
            {
                this.TransformRelativeRequestUri();
            }

            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested server.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Server test builder to test the returned HTTP response.</returns>
        public IServerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Tests for a particular HTTP response message.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        public IHttpHandlerResponseMessageWithTimeTestBuilder ShouldReturnHttpResponseMessage()
        {
            var serverHandler = new ServerHttpMessageHandler(this.client, this.disposeServer);
            using (var invoker = new HttpMessageInvoker(serverHandler, true))
            {
                var stopwatch = Stopwatch.StartNew();

                var httpResponseMessage = invoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;

                stopwatch.Stop();

                if (this.disposeServer && this.server != null)
                {
                    this.server.Dispose();
                }

                return new HttpHandlerResponseMessageWithTimeTestBuilder(serverHandler, httpResponseMessage, stopwatch.Elapsed);
            }
        }

        private void TransformRelativeRequestUri()
        {
            if (this.httpRequestMessage.RequestUri != null && !this.httpRequestMessage.RequestUri.IsAbsoluteUri)
            {
                this.httpRequestMessage.TransformToAbsoluteRequestUri();
            }
        }
    }
}