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

namespace MyWebApi.Builders.Contracts.HttpRequests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Uri;

    public interface IHttpRequestMessageBuilder
    {
        IAndHttpRequestMessageBuilder WithContent(HttpContent content);

        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream);

        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize);

        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes);

        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count);

        IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection);

        IAndHttpRequestMessageBuilder WithStringContent(string content);

        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding);

        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType);

        IAndHttpRequestMessageBuilder WithHeader(string name, string value);

        IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values);

        IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);

        IAndHttpRequestMessageBuilder WithHeaders(HttpRequestHeaders headers);

        IAndHttpRequestMessageBuilder WithMethod(string method);

        IAndHttpRequestMessageBuilder WithMethod(HttpMethod method);

        IAndHttpRequestMessageBuilder WithRequestUri(string location);

        IAndHttpRequestMessageBuilder WithRequestUri(Uri location);

        IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder);

        IAndHttpRequestMessageBuilder WithVersion(string version);

        IAndHttpRequestMessageBuilder WithVersion(Version version);
    }
}
