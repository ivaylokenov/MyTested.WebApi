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

namespace MyWebApi.Builders.Base
{
    using System.Net.Http;
    using Common.Extensions;
    using Contracts.Base;
    using Exceptions;

    /// <summary>
    /// Base class for handler test builders.
    /// </summary>
    public abstract class BaseHandlerTestBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHandlerTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">Instance of the HttpMessageHandler to be tested.</param>
        protected BaseHandlerTestBuilder(HttpMessageHandler handler)
        {
            this.Handler = handler;
        }

        /// <summary>
        /// Gets the HTTP message handler used in the testing.
        /// </summary>
        /// <value>Instance of HttpMessageHandler.</value>
        protected HttpMessageHandler Handler { get; private set; }

        /// <summary>
        /// Gets the HTTP message handler used in the testing.
        /// </summary>
        /// <returns>Instance of HttpMessageHandler.</returns>
        public HttpMessageHandler AndProvideTheHandler()
        {
            return this.Handler;
        }

        /// <summary>
        /// Sets inner HTTP message handler to the current message handler.
        /// </summary>
        /// <param name="innerHandler">Instance of HttpMessageHandler.</param>
        protected void SetInnerHandler(HttpMessageHandler innerHandler)
        {
            var handlerAsDelegatingHandler = this.Handler as DelegatingHandler;
            if (handlerAsDelegatingHandler == null)
            {
                throw new HttpHandlerAssertionException(string.Format(
                    "When adding inner handler {0} to {1}, expected {1} to be DelegatingHandler, but in fact was not.",
                    innerHandler.GetName(),
                    this.Handler.GetName()));
            }

            handlerAsDelegatingHandler.InnerHandler = innerHandler;
        }
    }
}
