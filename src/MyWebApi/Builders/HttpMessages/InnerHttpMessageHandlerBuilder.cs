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
    using Base;
    using Contracts.Handlers;

    /// <summary>
    /// Used for adding inner HTTP message handlers.
    /// </summary>
    public class InnerHttpMessageHandlerBuilder : BaseHandlerTestBuilder,
        IInnerHttpMessageHandlerBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InnerHttpMessageHandlerBuilder" /> class.
        /// </summary>
        /// <param name="handler">HTTP message handler on which the inner handler will be set.</param>
        public InnerHttpMessageHandlerBuilder(HttpMessageHandler handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Sets inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        public void WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            this.WithInnerHandler(new TInnerHandler());
        }

        /// <summary>
        /// Sets the provided instance as an inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        public void WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
        }

        /// <summary>
        /// Sets inner HTTP handler by using construction function to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        public void WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            this.WithInnerHandler(innerHandlerInstance);
        }

        /// <summary>
        /// Sets inner HTTP handler by using builder to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        public void WithInnerHandler<TInnerHandler>(Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            this.WithInnerHandler(newHttpMessageHandlerBuilder.Handler);
        }
    }
}
