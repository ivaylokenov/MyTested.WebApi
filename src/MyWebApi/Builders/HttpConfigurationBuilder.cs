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

namespace MyWebApi.Builders
{
    using System.Web.Http;
    using Contracts;
    using Servers;

    /// <summary>
    /// HTTP configuration builder.
    /// </summary>
    public class HttpConfigurationBuilder : IHttpConfigurationBuilder
    {
        private readonly HttpConfiguration httpConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConfigurationBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration instance used in the builder.</param>
        public HttpConfigurationBuilder(HttpConfiguration httpConfiguration)
        {
            this.httpConfiguration = httpConfiguration;
        }

        /// <summary>
        /// Starts HTTP server with the provided configuration.
        /// </summary>
        public void AndStartsServer()
        {
            new Server().Starts(this.httpConfiguration);
        }
    }
}
