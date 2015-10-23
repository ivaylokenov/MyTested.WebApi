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

namespace MyWebApi.Tests.Setups.Routes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class HeaderRouteConstraint : IHttpRouteConstraint
    {
        private readonly string name;
        private readonly string value;

        public HeaderRouteConstraint(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public bool Match(
            HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            var headers = request.Headers;
            var contentHeaders = request.Content != null ? request.Content.Headers : null;

            return (headers.Contains(this.name) && headers.GetValues(this.name).Contains(this.value))
                || (contentHeaders != null && contentHeaders.Contains(this.name) && contentHeaders.GetValues(this.name).Contains(this.value));
        }
    }
}
