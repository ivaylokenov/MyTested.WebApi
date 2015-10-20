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
        IShouldMapTestBuilder WithHttpMethod(string httpMethod);

        IShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod);

        IShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers);

        IShouldMapTestBuilder WithRequestHeaders(HttpRequestHeaders headers);

        IShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        IShouldMapTestBuilder WithContentHeaders(HttpContentHeaders headers);

        IShouldMapTestBuilder WithFormUrlEncodedContent(string content);

        IShouldMapTestBuilder WithJsonContent(string content);

        IShouldMapTestBuilder WithContent(string content, string mediaType);

        IShouldMapTestBuilder WithContent(string content, MediaTypeHeaderValue mediaType);

        IAndResolvedRouteTestBuilder То<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        IAndResolvedRouteTestBuilder То<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;

        void ToNotAllowedMethod();

        void ToNonExistingRoute();

        void ToIgnoredRoute();
    }
}
