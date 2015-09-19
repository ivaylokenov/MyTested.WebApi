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
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Exceptions;
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
    }
}
