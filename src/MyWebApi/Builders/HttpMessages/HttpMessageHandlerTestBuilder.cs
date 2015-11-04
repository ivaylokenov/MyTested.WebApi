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
    using System.Web.Http;
    using Base;
    using Contracts.Handlers;
    using Contracts.HttpRequests;
    using Contracts.HttpResponseMessages;
    using Utilities.Validators;

    public class HttpMessageHandlerTestBuilder
        : BaseHandlerTestBuilder, IHttpMessageHandlerBuilder, IHttpMessageHandlerTestBuilder
    {
        private HttpRequestMessage httpRequestMessage;
        private HttpConfiguration httpConfiguration;

        public HttpMessageHandlerTestBuilder(HttpMessageHandler handler)
            : base(handler)
        {
        }

        private HttpConfiguration HttpConfiguration
        {
            get { return this.httpConfiguration ?? MyWebApi.Configuration ?? new HttpConfiguration(); }
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            return this.WithInnerHandler(new TInnerHandler());
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
            return this;
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            return this.WithInnerHandler(innerHandlerInstance);
        }

        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(
            Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            return this.WithInnerHandler(newHttpMessageHandlerBuilder.AndProvideTheHandler());
        }

        public IHttpMessageHandlerBuilder WithHttpConfiguration(HttpConfiguration config)
        {
            this.httpConfiguration = config;
            return this;
        }

        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            var configuration = this.HttpConfiguration;
            this.httpRequestMessage.SetConfiguration(configuration);
            return this;
        }

        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        public IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            HttpResponseMessage httpResponseMessage = null;
            using (var httpMessageInvoker = new HttpMessageInvoker(this.Handler))
            {
                try
                {
                    httpResponseMessage = httpMessageInvoker.SendAsync(this.httpRequestMessage, CancellationToken.None).Result;
                }
                catch (Exception exception)
                {
                    CommonValidator.CheckForException(exception);
                }
            }

            return new HttpHandlerResponseMessageTestBuilder(
                this.Handler,
                httpResponseMessage);
        }

        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.httpRequestMessage;
        }

        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.HttpConfiguration;
        }
    }
}
