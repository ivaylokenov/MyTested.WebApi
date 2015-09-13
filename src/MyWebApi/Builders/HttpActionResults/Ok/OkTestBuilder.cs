﻿// MyWebApi - ASP.NET Web API Fluent Testing Framework
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

namespace MyWebApi.Builders.HttpActionResults.Ok
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Contracts.Formatters;
    using Contracts.HttpActionResults.Ok;
    using Common.Extensions;
    using Exceptions;
    using Models;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing the response model type of action.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class OkTestBuilder<TActionResult>
        : BaseResponseModelTestBuilder<TActionResult>, IAndOkTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public OkTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        public IAndOkTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as OkResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this;
        }

        public IAndOkTestBuilder WithDefaultContentNegotiator()
        {
            return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        }

        public IAndOkTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        {
            ContentNegotiatorValidator.ValidateContentNegotiator(
                this.GetActionResultAsDynamic(),
                contentNegotiator,
                new Action<string, string, string>(this.ThrowNewOkResultAssertionException));

            return this;
        }

        public IAndOkTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator, new()
        {
            return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        }

        public IAndOkTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.GetActionResultAsDynamic(),
                mediaTypeFormatter,
                new Action<string, string, string>(this.ThrowNewOkResultAssertionException));

            return this;
        }

        public IAndOkTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        public IAndOkTestBuilder ContainingDefaultFormatters()
        {
            return this.ContainingMediaTypeFormatters(MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters());
        }

        public IAndOkTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
                this.GetActionResultAsDynamic(),
                mediaTypeFormatters,
                new Action<string, string, string>(this.ThrowNewOkResultAssertionException));

            return this;
        }

        public IAndOkTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        {
            return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        }

        public IAndOkTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
                this.GetActionResultAsDynamic(),
                formattersBuilder,
                new Action<string, string, string>(this.ThrowNewOkResultAssertionException));

            return this;
        }

        public IOkTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewOkResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new OkResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected ok result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}