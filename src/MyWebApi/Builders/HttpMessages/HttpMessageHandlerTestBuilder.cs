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

namespace MyWebApi.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using Contracts.Handlers;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;

    public class HttpMessageHandlerTestBuilder : IHttpMessageHandlerBuilder, IHttpMessageHandlerTestBuilder
    {
        public HttpMessageHandlerTestBuilder(HttpMessageHandler handler)
        {
            this.Handler = handler;
        }

        public HttpMessageHandler Handler { get; private set; }

        public HttpRequestMessage HttpRequestMessage { get; private set; }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            return this.WithInnerHandler(new TInnerHandler());
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            var handlerAsDelegatingHandler = this.Handler as DelegatingHandler;
            if (handlerAsDelegatingHandler == null)
            {
                // TODO: throw
            }

            handlerAsDelegatingHandler.InnerHandler = innerHandler;
            return this;
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            return this.WithInnerHandler(innerHandlerInstance);
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(
            Action<IHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new HttpMessageHandlerTestBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            return this.WithInnerHandler(newHttpMessageHandlerBuilder.Handler);
        }

        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.HttpRequestMessage = requestMessage;
            return this;
        }

        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        public IHttpResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            var httpMessageInvoker = new HttpMessageInvoker(this.Handler);
            var httpMessageResult = httpMessageInvoker.SendAsync(this.HttpRequestMessage, CancellationToken.None).Result; // TODO: catch exception and add test builder, disposable
            return null;
        }

        private void ValidateHttpRequestMessage()
        {
            if (this.HttpRequestMessage == null)
            {
                // TODO: throw
            }
        }
    }
}
