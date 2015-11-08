// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.HttpMessages
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Exceptions;
    using Models;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing HTTP response message results from handlers.
    /// </summary>
    public class HttpHandlerResponseMessageTestBuilder 
        : BaseHandlerTestBuilder, IAndHttpHandlerResponseMessageTestBuilder
    {
        private readonly HttpResponseMessage httpResponseMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">Tested HTTP message handler.</param>
        /// <param name="httpResponseMessage">HTTP response result from the tested handler.</param>
        public HttpHandlerResponseMessageTestBuilder(
            HttpMessageHandler handler,
            HttpResponseMessage httpResponseMessage)
            : base(handler)
        {
            CommonValidator.CheckForNullReference(httpResponseMessage, errorMessageName: "HttpResponseMessage");
            this.httpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Tests whether certain type of response model is returned from the HTTP response message content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
        {
            var actualModel = HttpResponseMessageValidator.GetActualContentModel<TResponseModel>(
                this.httpResponseMessage.Content,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this.Handler,
                this,
                actualModel);
        }

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked HTTP response message content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
        {
            var actualModel = HttpResponseMessageValidator.WithResponseModel(
                this.httpResponseMessage.Content,
                expectedModel,
                this.ThrowNewHttpResponseMessageAssertionException,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this.Handler,
                this,
                actualModel);
        }

        /// <summary>
        /// Tests whether the content of the HTTP response message is of certain type.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithContentOfType<TContentType>()
            where TContentType : HttpContent
        {
            HttpResponseMessageValidator.WithContentOfType<TContentType>(
                this.httpResponseMessage.Content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether the content of the HTTP response message is the provided string.
        /// </summary>
        /// <param name="content">Expected string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithStringContent(string content)
        {
            HttpResponseMessageValidator.WithStringContent(
                this.httpResponseMessage.Content,
                content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message has the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.httpResponseMessage.Content,
                mediaTypeFormatter,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message has the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.WithMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// Tests whether the HTTP response message contains the default media type formatter provided by the framework.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithDefaultMediaTypeFormatter()
        {
            return this.WithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingHeader(string name)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and value.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingHeader(string name, string value)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and collection of value.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, values, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response headers provided by dictionary.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateHeadersCount(headers, this.httpResponseMessage.Headers, this.ThrowNewHttpResponseMessageAssertionException);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains content header with certain name.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingContentHeader(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
                name,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains content header with certain name and value.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="value">Value of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingContentHeader(string name, string value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
                name,
                value,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains content header with certain name and collection of value.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="values">Collection of values in the expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingContentHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
                name,
                values,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains content headers provided by dictionary.
        /// </summary>
        /// <param name="headers">Dictionary containing content headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ContainingContentHeaders(
            IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ValidateHeadersCount(
                headers,
                this.httpResponseMessage.Content.Headers,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeaders: true);

            headers.ForEach(h => this.ContainingContentHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpResponseMessageValidator.WithStatusCode(this.httpResponseMessage, statusCode, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version as string.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithVersion(string version)
        {
            var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewHttpResponseMessageAssertionException);
            return this.WithVersion(parsedVersion);
        }

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version.
        /// </summary>
        /// <param name="major">Major number in the expected version.</param>
        /// <param name="minor">Minor number in the expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithVersion(int major, int minor)
        {
            return this.WithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithVersion(Version version)
        {
            HttpResponseMessageValidator.WithVersion(this.httpResponseMessage, version, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message reason phrase is the same as the provided reason phrase as string.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithReasonPhrase(string reasonPhrase)
        {
            HttpResponseMessageValidator.WithReasonPhrase(this.httpResponseMessage, reasonPhrase, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message returns success status code between 200 and 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder WithSuccessStatusCode()
        {
            HttpResponseMessageValidator.WithSuccessStatusCode(this.httpResponseMessage, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IHttpHandlerResponseMessageTestBuilder AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Gets the HTTP response message used in the testing.
        /// </summary>
        /// <returns>Instance of HttpResponseMessage.</returns>
        public HttpResponseMessage AndProvideTheHttpResponseMessage()
        {
            return this.httpResponseMessage;
        }

        private void ThrowNewHttpResponseMessageAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpResponseMessageAssertionException(string.Format(
                    "When testing {0} expected HTTP response message result {1} {2}, but {3}.",
                    this.Handler.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }

        private ResponseModelAssertionException ThrowNewResponseModelAssertionException(string expectedResponseModel, string actualResponseModel)
        {
            return new ResponseModelAssertionException(string.Format(
                    "When testing {0} expected HTTP response message model to {1}, but {2}.",
                    this.Handler.GetName(),
                    expectedResponseModel,
                    actualResponseModel));
        }
    }
}
