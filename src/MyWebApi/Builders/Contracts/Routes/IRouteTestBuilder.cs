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

namespace MyWebApi.Builders.Contracts.Routes
{
    using System;
    using System.Net.Http;
    using HttpRequests;

    /// <summary>
    /// Used for building a route test.
    /// </summary>
    public interface IRouteTestBuilder
    {
        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(string location);

        /// <summary>
        /// Sets the route location to test.
        /// </summary>
        /// <param name="location">Location as Uri.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Uri location);

        /// <summary>
        /// Sets the route HTTP request message to test.
        /// </summary>
        /// <param name="requestMessage">Instance of type HttpRequestMessage.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(HttpRequestMessage requestMessage);

        /// <summary>
        /// Sets the route HTTP request message to test using a builder.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>Route test builder.</returns>
        IShouldMapTestBuilder ShouldMap(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder);
    }
}
