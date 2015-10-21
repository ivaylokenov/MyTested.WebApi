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
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Common.Extensions;
    using Contracts.Routes;

    public partial class ShouldMapTestBuilder
    {
        public IAndShouldMapTestBuilder WithHttpMethod(string httpMethod)
        {
            return this.WithHttpMethod(new HttpMethod(httpMethod));
        }

        public IAndShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            this.requestMessage.Method = httpMethod;
            return this;
        }

        public IAndShouldMapTestBuilder WithRequestHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        public IAndShouldMapTestBuilder WithRequestHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        public IAndShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithRequestHeader(h.Key, h.Value));
            return this;
        }

        public IAndShouldMapTestBuilder WithRequestHeaders(HttpRequestHeaders headers)
        {
            return this.WithRequestHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IAndShouldMapTestBuilder WithContentHeader(string name, string value)
        {
            this.requestMessage.Content.Headers.Add(name, value);
            return this;
        }

        public IAndShouldMapTestBuilder WithContentHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Content.Headers.Add(name, values);
            return this;
        }

        public IAndShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithContentHeader(h.Key, h.Value));
            return this;
        }

        public IAndShouldMapTestBuilder WithContentHeaders(HttpContentHeaders headers)
        {
            return this.WithContentHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IAndShouldMapTestBuilder WithFormUrlEncodedContent(string content)
        {
            this.SetRequestContent(content, MediaType.FormUrlEncoded);
            return this;
        }

        public IAndShouldMapTestBuilder WithJsonContent(string content)
        {
            this.SetRequestContent(content, MediaType.ApplicationJson);
            return this;
        }

        public IAndShouldMapTestBuilder WithContent(string content, string mediaType)
        {
            this.SetRequestContent(content, mediaType);
            return this;
        }

        public IAndShouldMapTestBuilder WithContent(string content, MediaTypeHeaderValue mediaType)
        {
            this.SetRequestContent(content, mediaType.MediaType);
            return this;
        }

        private void SetRequestContent(string content, string mediaType)
        {
            this.requestMessage.Content = new StringContent(content);
            this.requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
        }
    }
}
