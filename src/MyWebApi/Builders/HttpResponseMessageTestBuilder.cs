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
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Exceptions;
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

        public IAndHttpResponseMessageTestBuilder WithContentOfType<TContentType>()
            where TContentType : HttpContent
        {
            var expectedType = typeof (TContentType);
            var actualType = this.ActionResult.Content.GetType();
            if (Reflection.AreDifferentTypes(expectedType, actualType))
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "content",
                    string.Format("to be {0}", expectedType.GetName()),
                    string.Format("but was in fact {0}", actualType.GetName()));
            }

            return this;
        }

        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name)
        {
            if (!this.ActionResult.Headers.Contains(name))
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "headers",
                    string.Format("to contain {0}", name),
                    "but none was found");
            }

            return this;
        }

        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name, string value)
        {
            this.ContainingHeader(name);
            var headerValue = this.GetHeaderValues(name).FirstOrDefault(v => v == value);
            if (headerValue == null)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "headers",
                    string.Format("to contain {0} with {1} value", name, value),
                    "none was found");
            }

            return this;
        }

        public IAndHttpResponseMessageTestBuilder ContainingHeader(string name, IEnumerable<string> values)
        {
            this.ContainingHeader(name);
            var actualHeaderValuesWithExpectedName = this.GetHeaderValues(name);
            var expectedValues = values.ToList();
            var actualValuesCount = actualHeaderValuesWithExpectedName.Count;
            var expectedValuesCount = expectedValues.Count;
            if (expectedValuesCount != actualValuesCount)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "headers",
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
                        "headers",
                        string.Format("to have {0} with {1} value", name, expectedValue),
                        "none was found");
                }
            }

            return this;
        }

        public IAndHttpResponseMessageTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            this.ValidateHeadersCount(headers);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        public IAndHttpResponseMessageTestBuilder ContainingHeaders(HttpResponseHeaders headers)
        {
            return this.ContainingHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

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

        public IAndHttpResponseMessageTestBuilder WithVersion(string version)
        {
            var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewHttpResponseMessageAssertionException);
            return this.WithVersion(parsedVersion);
        }

        public IAndHttpResponseMessageTestBuilder WithVersion(int major, int minor)
        {
            return this.WithVersion(new Version(major, minor));
        }

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

        public IAndHttpResponseMessageTestBuilder WithReasonPhrase(string reasonPhrase)
        {
            var actualReasonPhrase = this.ActionResult.ReasonPhrase;
            if (actualReasonPhrase != reasonPhrase)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "reason phrase",
                    string.Format("to be {0}", reasonPhrase),
                    string.Format("instead received {0}", actualReasonPhrase));
            }

            return this;
        }

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

        private IList<string> GetHeaderValues(string name)
        {
            return this.ActionResult.Headers.First(h => h.Key == name).Value.ToList();
        }

        private void ValidateHeadersCount(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var actualHeadersCount = this.ActionResult.Headers.Count();
            var expectedHeadersCount = headers.Count();

            if (expectedHeadersCount != actualHeadersCount)
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                        "headers",
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
