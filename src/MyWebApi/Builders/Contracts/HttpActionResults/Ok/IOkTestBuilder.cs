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

namespace MyWebApi.Builders.Contracts.HttpActionResults.Ok
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Formatters;
    using Models;

    /// <summary>
    /// Used for testing ok result.
    /// </summary>
    public interface IOkTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithNoResponseModel();

        /// <summary>
        /// Tests whether ok result has the default content negotiator.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithDefaultContentNegotiator();

        /// <summary>
        /// Tests whether ok result has specific type of content negotiator.
        /// </summary>
        /// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator);

        /// <summary>
        /// Tests whether ok result has specific type of content negotiator by using generic definition.
        /// </summary>
        /// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator, new();

        /// <summary>
        /// Tests whether ok result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Tests whether ok result contains the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();

        /// <summary>
        /// Tests whether ok result contains the default media type formatters provided by the framework.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingDefaultFormatters();

        /// <summary>
        /// Tests whether ok result contains exactly the same types of media type formatters as the provided collection.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        /// <summary>
        /// Tests whether ok result contains exactly the same types of media type formatters as the provided parameters.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters);

        /// <summary>
        /// Tests whether ok result contains the media type formatters provided by builder.
        /// </summary>
        /// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder);
    }
}
