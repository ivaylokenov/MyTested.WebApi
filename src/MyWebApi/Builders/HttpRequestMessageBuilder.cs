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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Common.Extensions;
    using Contracts.HttpRequests;
    using Contracts.Uri;
    using Exceptions;
    using Uris;
    using Utilities.Validators;

    public class HttpRequestMessageBuilder : IAndHttpRequestMessageBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        public HttpRequestMessageBuilder()
        {
            this.requestMessage = new HttpRequestMessage();
        }

        public IAndHttpRequestMessageBuilder WithContent(HttpContent content)
        {
            this.requestMessage.Content = content;
            return this;
        }

        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream)
        {
            return this.WithContent(new StreamContent(stream));
        }

        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize)
        {
            return this.WithContent(new StreamContent(stream, bufferSize));
        }

        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes)
        {
            return this.WithContent(new ByteArrayContent(bytes));
        }

        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count)
        {
            return this.WithContent(new ByteArrayContent(bytes, offset, count));
        }

        public IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            return this.WithContent(new FormUrlEncodedContent(nameValueCollection));
        }

        public IAndHttpRequestMessageBuilder WithStringContent(string content)
        {
            return this.WithContent(new StringContent(content));
        }

        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding)
        {
            return this.WithContent(new StringContent(content, encoding));
        }

        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType)
        {
            return this.WithContent(new StringContent(content, encoding, mediaType));
        }

        public IAndHttpRequestMessageBuilder WithHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        public IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        public IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.requestMessage.Headers.Add(h.Key, h.Value));
            return this;
        }

        public IAndHttpRequestMessageBuilder WithHeaders(HttpRequestHeaders headers)
        {
            headers.ForEach(h => this.requestMessage.Headers.Add(h.Key, h.Value));
            return this;
        }

        public IAndHttpRequestMessageBuilder WithMethod(string method)
        {
            return this.WithMethod(new HttpMethod(method));
        }

        public IAndHttpRequestMessageBuilder WithMethod(HttpMethod method)
        {
            this.requestMessage.Method = method;
            return this;
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(string location)
        {
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this;
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(Uri location)
        {
            this.requestMessage.RequestUri = location;
            return this;
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder)
        {
            var mockedUriBuilder = new MockedUriBuilder();
            uriTestBuilder(mockedUriBuilder);
            this.requestMessage.RequestUri = mockedUriBuilder.GetUri();
            return this;
        }

        public IAndHttpRequestMessageBuilder WithVersion(string version)
        {
            return this.WithVersion(new Version(version));
        }

        public IAndHttpRequestMessageBuilder WithVersion(Version version)
        {
            this.requestMessage.Version = version;
            return this;
        }

        public IHttpRequestMessageBuilder And()
        {
            return this;
        }

        private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        {
            throw new InvalidHttpRequestMessageException(string.Format(
                "When building HttpRequestMessage expected {0} to be {1}, but instead received {2}",
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
