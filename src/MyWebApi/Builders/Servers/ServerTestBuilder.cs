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
    using Common.Servers;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;
    using Contracts.Servers;
    using HttpMessages;

    public class ServerTestBuilder : IServerBuilder, IServerTestBuilder
    {
        private readonly HttpMessageInvoker client;
        private readonly bool disposeServer;

        private HttpRequestMessage httpRequestMessage;

        public ServerTestBuilder(HttpMessageInvoker client, bool disposeServer = false)
        {
            this.client = client;
            this.disposeServer = disposeServer;
        }

        public IServerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            return this;
        }

        public IServerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        public IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            var serverHandler = new ServerHttpMessageHandler(this.client);
            var httpResponseMessage = serverHandler.HttpMessageInvoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;
            if (this.disposeServer)
            {
                this.client.Dispose();
            }

            return new HttpHandlerResponseMessageTestBuilder(serverHandler, httpResponseMessage);
        }
    }
}