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

namespace MyWebApi.Builders.Contracts.Handlers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Base;
    using HttpRequests;

    /// <summary>
    /// Used for building HTTP message handlers tests.
    /// </summary>
    public interface IHttpMessageHandlerBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Sets inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new();

        /// <summary>
        /// Sets the provided instance as an inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler;

        /// <summary>
        /// Sets inner HTTP handler by using construction function to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler;

        /// <summary>
        /// Sets inner HTTP handler by using builder to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerBuilder WithInnerHandler<TInnerHandler>(
            Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new();

        /// <summary>
        /// Sets the HTTP configuration for the current test case.
        /// </summary>
        /// <param name="config">Instance of HttpConfiguration.</param>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerBuilder WithHttpConfiguration(HttpConfiguration config);

        /// <summary>
        /// Adds HTTP request message to the tested handler.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same HTTP handler builder.</returns>
        IHttpMessageHandlerTestBuilder WithHttpRequestMessage(HttpRequestMessage requestMessage);

        /// <summary>
        /// Adds HTTP request message to the tested handler.
        /// </summary>
        /// <param name="httpRequestBuilder">Builder for HTTP request message.</param>
        /// <returns>The HTTP handler builder.</returns>
        IHttpMessageHandlerTestBuilder WithHttpRequestMessage(
            Action<IHttpRequestMessageBuilder> httpRequestBuilder);

        /// <summary>
        /// Gets the HTTP configuration used in the handler testing.
        /// </summary>
        /// <returns>Instance of HttpConfiguration.</returns>
        HttpConfiguration AndProvideTheHttpConfiguration();
    }
}
