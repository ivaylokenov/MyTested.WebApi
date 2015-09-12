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

namespace MyWebApi.Builders.Redirect
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.Redirect;
    using Contracts.Uri;
    using Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    /// <typeparam name="TRedirectResult">Type of redirect result - RedirectResult or RedirectToRouteResult.</typeparam>
    public class RedirectTestBuilder<TRedirectResult>
        : BaseTestBuilderWithActionResult<TRedirectResult>, IRedirectTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectTestBuilder{TRedirectResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public RedirectTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TRedirectResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder AtLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(location, this.ThrowNewRedirectResultAssertionException);
            return this.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder AtLocation(Uri location)
        {
            LocationValidator.ValidateUri(
                this.GetActionResultAsDynamic(),
                location,
                new Action<string, string, string>(this.ThrowNewRedirectResultAssertionException));

            return this;
        }

        /// <summary>
        /// Tests whether redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder)
        {
            LocationValidator.ValidateLocation(
                this.GetActionResultAsDynamic(),
                uriTestBuilder,
                new Action<string, string, string>(this.ThrowNewRedirectResultAssertionException));

            return this;
        }

        private void ThrowNewRedirectResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new RedirectResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected redirect result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
