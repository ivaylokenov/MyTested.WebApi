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

namespace MyWebApi.Builders.HttpActionResults.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Contracts.Formatters;
    using Contracts.HttpActionResults.Content;
    using Models;
    using Common.Extensions;
    using Exceptions;
    using Utilities.Validators;

    public class ContentTestBuilder<TContentResult>
        : BaseResponseModelTestBuilder<TContentResult>, IAndContentTestBuilder
    {
        public ContentTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TContentResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IAndContentTestBuilder WithDefaultContentNegotiator()
        {
            return this.WithContentNegotiatorOfType<DefaultContentNegotiator>();
        }

        public IAndContentTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator)
        {
            ContentNegotiatorValidator.ValidateContentNegotiator(
                this.GetActionResultAsDynamic(),
                contentNegotiator,
                new Action<string, string, string>(this.ThrowNewContentResultAssertionException));

            return this;
        }

        public IAndContentTestBuilder WithContentNegotiatorOfType<TContentNegotiator>() where TContentNegotiator : IContentNegotiator, new()
        {
            return this.WithContentNegotiator(Activator.CreateInstance<TContentNegotiator>());
        }

        public IAndContentTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.GetActionResultAsDynamic(),
                mediaTypeFormatter,
                new Action<string, string, string>(this.ThrowNewContentResultAssertionException));

            return this;
        }

        public IAndContentTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        public IAndContentTestBuilder ContainingDefaultFormatters()
        {
            return this.ContainingMediaTypeFormatters(MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters());
        }

        public IAndContentTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
                this.GetActionResultAsDynamic(),
                mediaTypeFormatters,
                new Action<string, string, string>(this.ThrowNewContentResultAssertionException));

            return this;
        }

        public IAndContentTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters)
        {
            return this.ContainingMediaTypeFormatters(mediaTypeFormatters.AsEnumerable());
        }

        public IAndContentTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
                this.GetActionResultAsDynamic(),
                formattersBuilder,
                new Action<string, string, string>(this.ThrowNewContentResultAssertionException));

            return this;
        }

        public IContentTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewContentResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new ContentResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected content result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
