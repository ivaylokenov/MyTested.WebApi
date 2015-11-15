// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Routes
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Contracts.HttpRequests;
    using Contracts.Routes;
    using HttpMessages;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public class RouteTestBuilder : BaseRouteTestBuilder, IRouteTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration to use for the route test.</param>
        public RouteTestBuilder(HttpConfiguration httpConfiguration)
            : base(httpConfiguration)
        {
        }

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(string location)
        {
            return new ShouldMapTestBuilder(
                this.HttpConfiguration,
                location);
        }

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as Uri.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(Uri location)
        {
            return this.ShouldMap(location.OriginalString);
        }

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="requestMessage">Instance of type HttpRequestMessage.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(HttpRequestMessage requestMessage)
        {
            return new ShouldMapTestBuilder(
                this.HttpConfiguration,
                requestMessage);
        }

        /// <summary>
        /// Sets the route HTTP request message to test using a builder.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Route test builder.</returns>
        public IShouldMapTestBuilder ShouldMap(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            var requestMessage = httpBuilder.GetHttpRequestMessage();
            return this.ShouldMap(requestMessage);
        }
    }
}
