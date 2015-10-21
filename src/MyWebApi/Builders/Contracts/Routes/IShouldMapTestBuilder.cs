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
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    public interface IShouldMapTestBuilder : IResolvedRouteTestBuilder
    {
        IAndShouldMapTestBuilder WithHttpMethod(string httpMethod);

        IAndShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod);

        IAndShouldMapTestBuilder WithRequestHeader(string name, string value);

        IAndShouldMapTestBuilder WithRequestHeader(string name, IEnumerable<string> values);

        IAndShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers);

        IAndShouldMapTestBuilder WithRequestHeaders(HttpRequestHeaders headers);

        IAndShouldMapTestBuilder WithContentHeader(string name, string value);

        IAndShouldMapTestBuilder WithContentHeader(string name, IEnumerable<string> values);

        IAndShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        IAndShouldMapTestBuilder WithContentHeaders(HttpContentHeaders headers);

        IAndShouldMapTestBuilder WithFormUrlEncodedContent(string content);

        IAndShouldMapTestBuilder WithJsonContent(string content);

        IAndShouldMapTestBuilder WithContent(string content, string mediaType);

        IAndShouldMapTestBuilder WithContent(string content, MediaTypeHeaderValue mediaType);

        IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;

        void ToNotAllowedMethod();

        void ToNonExistingRoute();

        void ToIgnoredRoute();
    }
}
