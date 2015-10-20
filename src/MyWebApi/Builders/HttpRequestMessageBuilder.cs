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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Common.Extensions;
    using Contracts.HttpRequests;
    using Contracts.Uris;
    using Exceptions;
    using Uris;
    using Utilities.Validators;

    /// <summary>
    /// Used for building HTTP request message.
    /// </summary>
    public class HttpRequestMessageBuilder : IAndHttpRequestMessageBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessageBuilder" /> class.
        /// </summary>
        public HttpRequestMessageBuilder()
        {
            this.requestMessage = new HttpRequestMessage();
        }

        /// <summary>
        /// Adds HTTP content to the built HTTP request message.
        /// </summary>
        /// <param name="content">HTTP content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContent(HttpContent content)
        {
            this.requestMessage.Content = content;
            return this;
        }

        /// <summary>
        /// Adds HTTP stream content to the built HTTP request message.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream)
        {
            return this.WithContent(new StreamContent(stream));
        }

        /// <summary>
        /// Adds HTTP stream content to the built HTTP request message.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <param name="bufferSize">Buffer size of the added HTTP stream content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize)
        {
            return this.WithContent(new StreamContent(stream, bufferSize));
        }

        /// <summary>
        /// Adds HTTP byte array content to the built HTTP request message.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes)
        {
            return this.WithContent(new ByteArrayContent(bytes));
        }

        /// <summary>
        /// Adds HTTP byte array content to the built HTTP request message.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <param name="offset">Offset in the HTTP byte array content.</param>
        /// <param name="count">The number of bytes in the HTTP byte array content to use.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count)
        {
            return this.WithContent(new ByteArrayContent(bytes, offset, count));
        }

        /// <summary>
        /// Adds HTTP form URL encoded content to the built HTTP request message.
        /// </summary>
        /// <param name="nameValueCollection">Name value pairs collection.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            return this.WithContent(new FormUrlEncodedContent(nameValueCollection));
        }

        public IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(string queryString)
        {
            return this.WithContent(new StringContent(queryString, Encoding.UTF8, MediaType.FormUrlEncoded));
        }

        public IAndHttpRequestMessageBuilder WithJsonContent(string jsonContent)
        {
            return this.WithContent(new StringContent(jsonContent, Encoding.UTF8, MediaType.ApplicationJson));
        }

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content)
        {
            return this.WithContent(new StringContent(content));
        }

        public IAndHttpRequestMessageBuilder WithStringContent(string content, string mediaType)
        {
            return this.WithContent(new StringContent(content, Encoding.UTF8, mediaType));
        }

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding)
        {
            return this.WithContent(new StringContent(content, encoding));
        }

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType)
        {
            return this.WithContent(new StringContent(content, encoding, mediaType));
        }

        /// <summary>
        /// Adds header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Adds collection of headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Headers represented by HttpRequestHeaders type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeaders(HttpRequestHeaders headers)
        {
            return this.WithHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        /// <summary>
        /// Adds content header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeader(string name, string value)
        {
            this.ValidateContentBeforeAddingContentHeaders();
            this.requestMessage.Content.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds content header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeader(string name, IEnumerable<string> values)
        {
            this.ValidateContentBeforeAddingContentHeaders();
            this.requestMessage.Content.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Adds collection of content headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            this.ValidateContentBeforeAddingContentHeaders();
            this.requestMessage.Content.Headers.Clear();
            headers.ForEach(h => this.WithContentHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds content headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Headers represented by HttpRequestHeaders type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeaders(HttpContentHeaders headers)
        {
            return this.WithContentHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        /// <summary>
        /// Adds method to the built HTTP request message.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithMethod(string method)
        {
            return this.WithMethod(new HttpMethod(method));
        }

        /// <summary>
        /// Adds method to the built HTTP request message.
        /// </summary>
        /// <param name="method">HTTP method represented by HttpMethod type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithMethod(HttpMethod method)
        {
            this.requestMessage.Method = method;
            return this;
        }

        /// <summary>
        /// Adds request location to the built HTTP request message.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(string location)
        {
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this;
        }

        /// <summary>
        /// Adds request location to the built HTTP request message.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(Uri location)
        {
            this.requestMessage.RequestUri = location;
            return this;
        }

        /// <summary>
        /// Adds request location provided by a builder to the HTTP request message.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder)
        {
            var mockedUriBuilder = new MockedUriBuilder();
            uriTestBuilder(mockedUriBuilder);
            this.requestMessage.RequestUri = mockedUriBuilder.GetUri();
            return this;
        }

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="version">HTTP version provided by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(string version)
        {
            var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewInvalidHttpRequestMessageException);
            return this.WithVersion(parsedVersion);
        }

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="major">Major number in the provided version.</param>
        /// <param name="minor">Minor number in the provided version.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(int major, int minor)
        {
            return this.WithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="version">HTTP version provided by Version type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(Version version)
        {
            this.requestMessage.Version = version;
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when building HTTP request message.
        /// </summary>
        /// <returns>The same HTTP request message builder.</returns>
        public IHttpRequestMessageBuilder AndAlso()
        {
            return this;
        }

        internal HttpRequestMessage GetHttpRequestMessage()
        {
            return this.requestMessage;
        }

        private void ValidateContentBeforeAddingContentHeaders()
        {
            if (this.requestMessage.Content == null)
            {
                this.ThrowNewInvalidHttpRequestMessageException(
                    "content",
                    "initialized and set in order to add content headers",
                    "null");
            }
        }

        private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        {
            throw new InvalidHttpRequestMessageException(string.Format(
                "When building HttpRequestMessage expected {0} to be {1}, but instead received {2}.",
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
