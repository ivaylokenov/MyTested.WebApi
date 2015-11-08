// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
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

    /// <summary>
    /// Used for testing HTTP message handlers.
    /// </summary>
    public class HttpMessageHandlerTestBuilder
        : BaseHandlerTestBuilder, IHttpMessageHandlerBuilder, IHttpMessageHandlerTestBuilder
    {
        private HttpRequestMessage httpRequestMessage;
        private HttpConfiguration httpConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">HTTP handler which will be tested.</param>
        public HttpMessageHandlerTestBuilder(HttpMessageHandler handler)
            : base(handler)
        {
        }

        private HttpConfiguration HttpConfiguration
        {
            get { return this.httpConfiguration ?? MyWebApi.Configuration ?? new HttpConfiguration(); }
        }

        /// <summary>
        /// Sets inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            return this.WithInnerHandler(new TInnerHandler());
        }

        /// <summary>
        /// Sets the provided instance as an inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
            return this;
        }

        /// <summary>
        /// Sets inner HTTP handler by using construction function to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            return this.WithInnerHandler(innerHandlerInstance);
        }

        /// <summary>
        /// Sets inner HTTP handler by using builder to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(
            Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            return this.WithInnerHandler(newHttpMessageHandlerBuilder.AndProvideTheHandler());
        }

        /// <summary>
        /// Sets the HTTP configuration for the current test case.
        /// </summary>
        /// <param name="config">Instance of HttpConfiguration.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerBuilder WithHttpConfiguration(HttpConfiguration config)
        {
            this.httpConfiguration = config;
            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested handler.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.httpRequestMessage = requestMessage;
            var configuration = this.HttpConfiguration;
            this.httpRequestMessage.SetConfiguration(configuration);
            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested handler.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>The same HTTP handler builder.</returns>
        public IHttpMessageHandlerTestBuilder WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Tests the HTTP handler for returning HTTP response message successfully.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
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

        /// <summary>
        /// Gets the HTTP request message used in the handler testing.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.httpRequestMessage;
        }

        /// <summary>
        /// Gets the HTTP configuration used in the handler testing.
        /// </summary>
        /// <returns>Instance of HttpConfiguration.</returns>
        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.HttpConfiguration;
        }
    }
}
