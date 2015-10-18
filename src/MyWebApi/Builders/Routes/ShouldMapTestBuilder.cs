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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Routes;

    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IShouldMapTestBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            string url)
            : base(httpConfiguration)
        {
            this.requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        }

        public IShouldMapTestBuilder WithHttpMethod(string httpMethod)
        {
            return this.WithHttpMethod(new HttpMethod(httpMethod));
        }

        public IShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            this.requestMessage.Method = httpMethod;
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.requestMessage.Headers.Add(h.Key, h.Value));
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeaders(HttpRequestHeaders headers)
        {
            return this.WithRequestHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.requestMessage.Content.Headers.Add(h.Key, h.Value));
            return this;
        }

        public IShouldMapTestBuilder WithContentHeaders(HttpRequestHeaders headers)
        {
            return this.WithContentHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IShouldMapTestBuilder WithFormUrlEncodedBody(string body)
        {
            this.SetRequestBody(body, MediaType.FormUrlEncoded);
            return this;
        }

        public IShouldMapTestBuilder WithJsonBody(string body)
        {
            this.SetRequestBody(body, MediaType.ApplicationJson);
            return this;
        }

        public IShouldMapTestBuilder WithBody(string body, string mediaType)
        {
            this.SetRequestBody(body, mediaType);
            return this;
        }

        public IShouldMapTestBuilder WithBody(string body, MediaTypeHeaderValue mediaType)
        {
            this.SetRequestBody(body, mediaType.MediaType);
            return this;
        }

        public void То<ТController>(Expression<Func<ТController, object>> actionCall)
        {

        }

        public void То<ТController>(Expression<Action<ТController>> actionCall)
        {

        }

        private void SetRequestBody(string body, string mediaType)
        {
            this.requestMessage.Content = new StringContent(body);
            this.requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
        }
    }
}
