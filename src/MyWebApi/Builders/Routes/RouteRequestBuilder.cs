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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Common.Extensions;
    using Contracts.Routes;

    /// <summary>
    /// Used for building a request for route test.
    /// </summary>
    public partial class ShouldMapTestBuilder
    {
        /// <summary>
        /// Adds HTTP method to the built route test.
        /// </summary>
        /// <param name="httpMethod">HTTP method represented by string.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithHttpMethod(string httpMethod)
        {
            return this.WithHttpMethod(new HttpMethod(httpMethod));
        }

        /// <summary>
        /// Adds HTTP method to the built route test.
        /// </summary>
        /// <param name="httpMethod">HTTP method represented by HttpMethod.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            this.requestMessage.Method = httpMethod;
            return this;
        }

        /// <summary>
        /// Add HTTP header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithRequestHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Add HTTP header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithRequestHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Adds collection of HTTP headers to the built route test.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithRequestHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Add HTTP content header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the content header.</param>
        /// <param name="value">Value of the content header.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithContentHeader(string name, string value)
        {
            this.requestMessage.Content.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Add HTTP content header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the content header.</param>
        /// <param name="values">Collection of values for the content header.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithContentHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Content.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Adds collection of HTTP content headers to the built route test.
        /// </summary>
        /// <param name="headers">Dictionary of content headers to add.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithContentHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds URL encoded content to the built route test.
        /// </summary>
        /// <param name="content">URL encoded content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithFormUrlEncodedContent(string content)
        {
            this.SetRequestContent(content, MediaType.FormUrlEncoded);
            return this;
        }

        /// <summary>
        /// Adds JSON content to the built route test.
        /// </summary>
        /// <param name="content">JSON content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithJsonContent(string content)
        {
            this.SetRequestContent(content, MediaType.ApplicationJson);
            return this;
        }

        /// <summary>
        /// Adds content to the built route test.
        /// </summary>
        /// <param name="content">Content represented by string.</param>
        /// <param name="mediaType">Media type of the content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        public IAndShouldMapTestBuilder WithContent(string content, string mediaType)
        {
            this.SetRequestContent(content, mediaType);
            return this;
        }

        /// <summary>
        /// Adds content to the built route test.
        /// </summary>
        /// <param name="content">Content represented by string.</param>
        /// <param name="mediaType">Media type of the content represented by MediaTypeHeaderValue.</param>
        /// <returns>The same route test builder.</returns>
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
