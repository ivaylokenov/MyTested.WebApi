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

namespace MyWebApi.Builders.ExceptionErrors
{
    using System.Net;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ExceptionErrors;
    using Contracts.HttpResponseMessages;
    using Exceptions;

    /// <summary>
    /// Used for testing expected HttpResponseException.
    /// </summary>
    public class HttpResponseExceptionTestBuilder : BaseTestBuilderWithCaughtException, IHttpResponseExceptionTestBuilder
    {
        private readonly HttpResponseException httpResponseException;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseExceptionTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Actual received HttpResponseException.</param>
        public HttpResponseExceptionTestBuilder(
            ApiController controller,
            string actionName,
            HttpResponseException caughtException)
            : base(controller, actionName, caughtException)
        {
            this.httpResponseException = caughtException;
        }

        /// <summary>
        /// Tests whether caught HttpResponseException has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException WithStatusCode(HttpStatusCode statusCode)
        {
            var actualStatusCode = this.httpResponseException.Response.StatusCode;
            if (actualStatusCode != statusCode)
            {
                throw new HttpStatusCodeResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected HttpResponseException to have {2} ({3}) status code, but received {4} ({5}).",
                    this.ActionName,
                    this.Controller.GetName(),
                    (int)statusCode,
                    statusCode,
                    (int)actualStatusCode,
                    actualStatusCode));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Provides methods to test whether caught HttpResponseException has specific HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        public IHttpResponseMessageTestBuilder WithHttpResponseMessage()
        {
            return new HttpResponseMessageTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.httpResponseException.Response);
        }
    }
}
