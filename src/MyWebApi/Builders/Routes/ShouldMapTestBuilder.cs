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
    using System.Net.Http;
    using System.Web.Http;
    using Common;
    using Contracts.Routes;

    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IShouldMapTestBuilder
    {
        private readonly string url;
        private readonly HttpMethod httpMethod;

        private RequestBodyFormat requestBodyFormat;
        private string requestBody;

        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            string url,
            HttpMethod httpMethod)
            : base(httpConfiguration)
        {
            this.url = url;
            this.httpMethod = httpMethod;
            this.requestBodyFormat = RequestBodyFormat.None;
        }

        public IShouldMapTestBuilder WithFormUrlBody(string body)
        {
            requestBody = body;
            requestBodyFormat = RequestBodyFormat.FormUrl;
            return this;
        }

        public IShouldMapTestBuilder WithJsonBody(string body)
        {
            requestBody = body;
            requestBodyFormat = RequestBodyFormat.Json;
            return this;
        }
    }
}
