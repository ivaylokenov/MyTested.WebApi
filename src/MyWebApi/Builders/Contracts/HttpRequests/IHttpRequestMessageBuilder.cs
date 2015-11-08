// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.HttpRequests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using Uris;

    /// <summary>
    /// Used for building HTTP request message.
    /// </summary>
    public interface IHttpRequestMessageBuilder
    {
        /// <summary>
        /// Adds HTTP content to the built HTTP request message.
        /// </summary>
        /// <param name="content">HTTP content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContent(HttpContent content);
        
        /// <summary>
        /// Adds HTTP stream content to the built HTTP request message.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream);

        /// <summary>
        /// Adds HTTP stream content to the built HTTP request message.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <param name="bufferSize">Buffer size of the added HTTP stream content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize);

        /// <summary>
        /// Adds HTTP byte array content to the built HTTP request message.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes);

        /// <summary>
        /// Adds HTTP byte array content to the built HTTP request message.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <param name="offset">Offset in the HTTP byte array content.</param>
        /// <param name="count">The number of bytes in the HTTP byte array content to use.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count);

        /// <summary>
        /// Adds HTTP form URL encoded content to the built HTTP request message.
        /// </summary>
        /// <param name="nameValueCollection">Name value pairs collection.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection);

        /// <summary>
        /// Adds HTTP form URL encoded content to the built HTTP request message.
        /// </summary>
        /// <param name="queryString">String representing the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(string queryString);

        /// <summary>
        /// Adds JSON content to the built HTTP request message.
        /// </summary>
        /// <param name="jsonContent">JSON string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithJsonContent(string jsonContent);

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content);

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, string mediaType);

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding);

        /// <summary>
        /// Adds HTTP string content to the built HTTP request message.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType);

        /// <summary>
        /// Adds header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeader(string name, string value);

        /// <summary>
        /// Adds header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds collection of headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Adds content header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeader(string name, string value);

        /// <summary>
        /// Adds content header to the built HTTP request message.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds collection of content headers to the built HTTP request message.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Adds method to the built HTTP request message.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithMethod(string method);

        /// <summary>
        /// Adds method to the built HTTP request message.
        /// </summary>
        /// <param name="method">HTTP method represented by HttpMethod type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithMethod(HttpMethod method);

        /// <summary>
        /// Adds request location to the built HTTP request message.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(string location);

        /// <summary>
        /// Adds request location to the built HTTP request message.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(Uri location);

        /// <summary>
        /// Adds request location provided by a builder to the HTTP request message.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="version">HTTP version provided by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(string version);

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="major">Major number in the provided version.</param>
        /// <param name="minor">Minor number in the provided version.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(int major, int minor);

        /// <summary>
        /// Adds HTTP version to the built HTTP request message.
        /// </summary>
        /// <param name="version">HTTP version provided by Version type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(Version version);
    }
}
