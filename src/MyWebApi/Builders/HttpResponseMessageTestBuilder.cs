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
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Exceptions;
    using Models;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing HTTP response message results.
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
            var actualModel = this.GetActualContentModel<TResponseModel>();
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
            this.WithContentOfType<ObjectContent>();
            var actualModel = this.GetActualContentModel<TResponseModel>();
            if (expectedModel != actualModel)
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected HTTP response message model to be the given model, but in fact it was a different model.",
                            this.ActionName,
                            this.Controller.GetName()));
            }

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
            var expectedType = typeof(TContentType);
            var actualType = this.ActionResult.Content.GetType();
            if (Reflection.AreDifferentTypes(expectedType, actualType))
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "content",
                    string.Format("to be {0}", expectedType.ToFriendlyTypeName()),
                    string.Format("was in fact {0}", actualType.ToFriendlyTypeName()));
            }

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
            this.ContainingHeader(this.ActionResult.Headers, name);
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
            this.ContainingHeader(this.ActionResult.Headers, name, value);
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
            this.ContainingHeader(this.ActionResult.Headers, name, values);
            return this;
        }

        /// <summary>
        /// Tests whether the HTTP response message contains response headers provided by dictionary.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            this.ValidateHeadersCount(headers, this.ActionResult.Headers);
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
            this.ValidateContent();
            this.ContainingHeader(this.ActionResult.Content.Headers, name, isContentHeader: true);
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
            this.ValidateContent();
            this.ContainingHeader(this.ActionResult.Content.Headers, name, value, isContentHeader: true);
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
            this.ValidateContent();
            this.ContainingHeader(this.ActionResult.Content.Headers, name, values, isContentHeader: true);
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
            this.ValidateContent();
            this.ValidateHeadersCount(headers, this.ActionResult.Content.Headers, isContentHeaders: true);
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
            var actualStatusCode = this.ActionResult.StatusCode;
            if (actualStatusCode != statusCode)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "status code",
                    string.Format("to be {0} ({1})", (int)statusCode, statusCode),
                    string.Format("instead received {0} ({1})", (int)actualStatusCode, actualStatusCode));
            }

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
            var actualVersion = this.ActionResult.Version;
            if (actualVersion != version)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "version",
                    string.Format("to be {0}", version),
                    string.Format("instead received {0}", actualVersion));
            }

            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message reason phrase is the same as the provided reason phrase as string.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithReasonPhrase(string reasonPhrase)
        {
            var actualReasonPhrase = this.ActionResult.ReasonPhrase;
            if (actualReasonPhrase != reasonPhrase)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "reason phrase",
                    string.Format("to be '{0}'", reasonPhrase),
                    string.Format("instead received '{0}'", actualReasonPhrase));
            }

            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message returns success status code between 200 and 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder WithSuccessStatusCode()
        {
            if (!this.ActionResult.IsSuccessStatusCode)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "status code",
                    string.Format("to be between 200 and 299"),
                    string.Format("it was not"));
            }

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

        private TResponseModel GetActualContentModel<TResponseModel>()
        {
            var responseModel = ((ObjectContent)this.ActionResult.Content).Value;
            try
            {
                return (TResponseModel)responseModel;
            }
            catch (InvalidCastException)
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected HTTP response message model to be a {2}, but instead received a {3}.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName(),
                            responseModel.GetType().ToFriendlyTypeName()));
            }
        }

        private void ValidateContent()
        {
            if (this.ActionResult.Content == null)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "content",
                    "to be initialized and set",
                    "it was null and no content headers were found");
            }
        }

        private void ContainingHeader(HttpHeaders headers, string name, bool isContentHeader = false)
        {
            if (!headers.Contains(name))
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0}", name),
                    "none was found");
            }
        }

        private void ContainingHeader(HttpHeaders headers, string name, string value, bool isContentHeader = false)
        {
            this.ContainingHeader(name);
            var headerValue = this.GetHeaderValues(headers, name).FirstOrDefault(v => v == value);
            if (headerValue == null)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} value", name, value),
                    "none was found");
            }
        }

        private void ContainingHeader(
            HttpHeaders headers,
            string name,
            IEnumerable<string> values,
            bool isContentHeader = false)
        {
            this.ContainingHeader(headers, name, isContentHeader);
            var actualHeaderValuesWithExpectedName = this.GetHeaderValues(headers, name);
            var expectedValues = values.ToList();
            var actualValuesCount = actualHeaderValuesWithExpectedName.Count;
            var expectedValuesCount = expectedValues.Count;
            if (expectedValuesCount != actualValuesCount)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} values", name, expectedValuesCount),
                    string.Format("instead found {0}", actualValuesCount));
            }

            var sortedActualValues = actualHeaderValuesWithExpectedName.OrderBy(v => v).ToList();
            var sortedExpectedValues = expectedValues.OrderBy(v => v).ToList();

            for (int i = 0; i < sortedActualValues.Count; i++)
            {
                var actualValue = sortedActualValues[i];
                var expectedValue = sortedExpectedValues[i];
                if (actualValue != expectedValue)
                {
                    this.ThrowNewHttpResponseMessageAssertionException(
                        isContentHeader ? "content headers" : "headers",
                        string.Format("to have {0} with {1} value", name, expectedValue),
                        "none was found");
                }
            }
        }

        private IList<string> GetHeaderValues(HttpHeaders headers, string name)
        {
            return headers.First(h => h.Key == name).Value.ToList();
        }

        private void ValidateHeadersCount(
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> expectedHeaders,
            HttpHeaders actualHeaders,
            bool isContentHeaders = false)
        {
            var actualHeadersCount = actualHeaders.Count();
            var expectedHeadersCount = expectedHeaders.Count();

            if (expectedHeadersCount != actualHeadersCount)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    isContentHeaders ? "content headers" : "headers",
                    string.Format("to be {0}", expectedHeadersCount),
                    string.Format("were in fact {0}", actualHeadersCount));
            }
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
    }
}
