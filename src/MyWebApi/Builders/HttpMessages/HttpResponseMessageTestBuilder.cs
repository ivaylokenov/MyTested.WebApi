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

namespace MyWebApi.Builders.HttpMessages
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Base;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Models;
    using Common.Extensions;
    using Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing HTTP response message results from actions.
    /// </summary>
    public class HttpResponseMessageTestBuilder
        : BaseTestBuilderWithActionResult<HttpResponseMessage>, IAndHttpResponseMessageTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">HTTP response result from the tested action.</param>
        public HttpResponseMessageTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            HttpResponseMessage actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether certain type of response model is returned from the HTTP response message content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
        {
            this.WithContentOfType<ObjectContent>();
            var actualModel = HttpResponseMessageValidator.GetActualContentModel<TResponseModel>(
                this.ActionResult.Content,
                this.ThrowNewResponseModelAssertionException);

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        /// <summary>
        /// Tests whether an object is returned from the invoked HTTP response message content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class
        {
            var actualModel = HttpResponseMessageValidator.WithResponseModel(
                this.ActionResult.Content,
                expectedModel,
                this.ThrowNewHttpResponseMessageAssertionException,
                this.ThrowNewResponseModelAssertionException);

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        /// <summary>
        /// Tests whether the content of the HTTP response message is of certain type.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithContentOfType<TContentType>()
            where TContentType : HttpContent
        {
            HttpResponseMessageValidator.WithContentOfType<TContentType>(
                this.ActionResult.Content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message has the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.ActionResult.Content,
                mediaTypeFormatter,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message has the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.WithMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// Tests whether the HTTP response message contains the default media type formatter provided by the framework.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithDefaultMediaTypeFormatter()
        {
            return this.WithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and value.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name, string value)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and collection of value.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, values, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response headers provided by dictionary.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateHeadersCount(headers, this.ActionResult.Headers, this.ThrowNewHttpResponseMessageAssertionException);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains content header with certain name.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingContentHeader(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
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
        public IAndHttpResponseMessageTestBuilder ContainingContentHeader(string name, string value)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
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
        public IAndHttpResponseMessageTestBuilder ContainingContentHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
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
        public IAndHttpResponseMessageTestBuilder ContainingContentHeaders(
            IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ValidateHeadersCount(
                headers,
                this.ActionResult.Content.Headers,
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
        public IAndHttpResponseMessageTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpResponseMessageValidator.WithStatusCode(this.ActionResult, statusCode, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version as string.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithVersion(string version)
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
        public IAndHttpResponseMessageTestBuilder WithVersion(int major, int minor)
        {
            return this.WithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithVersion(Version version)
        {
            HttpResponseMessageValidator.WithVersion(this.ActionResult, version, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message reason phrase is the same as the provided reason phrase as string.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithReasonPhrase(string reasonPhrase)
        {
            HttpResponseMessageValidator.WithReasonPhrase(this.ActionResult, reasonPhrase, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message returns success status code between 200 and 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithSuccessStatusCode()
        {
            HttpResponseMessageValidator.WithSuccessStatusCode(this.ActionResult, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IHttpResponseMessageTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewHttpResponseMessageAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpResponseMessageAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP response message result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }

        private ResponseModelAssertionException ThrowNewResponseModelAssertionException(string expectedResponseModel, string actualResponseModel)
        {
            return new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP response message model to {2}, but {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedResponseModel,
                    actualResponseModel));
        }
    }
}
