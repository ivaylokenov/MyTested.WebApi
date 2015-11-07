// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Builders.Servers
{
    using System;
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
        private readonly HttpMessageInvoker client;
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
            HttpMessageInvoker client,
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
        public IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            var serverHandler = new ServerHttpMessageHandler(this.client, this.disposeServer);
            using (var invoker = new HttpMessageInvoker(serverHandler, true))
            {
                var httpResponseMessage = invoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;
                if (this.disposeServer && this.server != null)
                {
                    this.server.Dispose();
                }

                return new HttpHandlerResponseMessageTestBuilder(serverHandler, httpResponseMessage);
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