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
    using Contracts.Handlers;

    public class HttpMessageHandlerTestBuilder : IHttpMessageHandlerTestBuilder
    {
        public HttpMessageHandlerTestBuilder(HttpMessageHandler handler)
        {
            this.Handler = handler;
        }

        public HttpMessageHandler Handler { get; private set; }

        public IHttpMessageHandlerTestBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            return this.WithInnerHandler(new TInnerHandler());
        }

        public IHttpMessageHandlerTestBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            var handlerAsDelegatingHandler = this.Handler as DelegatingHandler;
            if (handlerAsDelegatingHandler == null)
            {
                // TODO: throw
                return null;
            }

            handlerAsDelegatingHandler.InnerHandler = innerHandler;
            return this;
        }

        public IHttpMessageHandlerTestBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            return this.WithInnerHandler(innerHandlerInstance);
        }

        public IHttpMessageHandlerTestBuilder WithInnerHandler<TInnerHandler>(
            Action<IHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new HttpMessageHandlerTestBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            return this.WithInnerHandler(newHttpMessageHandlerBuilder.Handler);
        }

        public HttpRequestMessage HttpRequestMessage { get; private set; }
    }
}
