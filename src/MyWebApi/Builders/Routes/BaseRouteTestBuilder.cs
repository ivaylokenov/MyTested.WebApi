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

namespace MyWebApi.Builders.Routes
{
    using System.Web.Http;

    /// <summary>
    /// Base class for all route test builders.
    /// </summary>
    public abstract class BaseRouteTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRouteTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">Instance of HttpConfiguration.</param>
        protected BaseRouteTestBuilder(HttpConfiguration httpConfiguration)
        {
            this.HttpConfiguration = httpConfiguration;
        }

        /// <summary>
        /// Gets the HTTP configuration used in the route test.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        protected HttpConfiguration HttpConfiguration { get; private set; }
    }
}
