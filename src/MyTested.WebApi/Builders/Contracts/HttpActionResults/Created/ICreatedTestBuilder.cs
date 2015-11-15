// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Created
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Formatters;
    using Models;
    using Uris;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    public interface ICreatedTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether created result has the default content negotiator.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithDefaultContentNegotiator();

        /// <summary>
        /// Tests whether created result has specific type of content negotiator.
        /// </summary>
        /// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithContentNegotiator(IContentNegotiator contentNegotiator);

        /// <summary>
        /// Tests whether created result has specific type of content negotiator by using generic definition.
        /// </summary>
        /// <typeparam name="TContentNegotiator">Type of IContentNegotiator.</typeparam>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder WithContentNegotiatorOfType<TContentNegotiator>()
            where TContentNegotiator : IContentNegotiator, new();

        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(string location);

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Uri location);

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Tests whether created result contains the provided type of media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();

        /// <summary>
        /// Tests whether created result contains the default media type formatters provided by the framework.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingDefaultFormatters();

        /// <summary>
        /// Tests whether created result contains exactly the same types of media type formatters as the provided collection.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters);

        /// <summary>
        /// Tests whether created result contains exactly the same types of media type formatters as the provided parameters.
        /// </summary>
        /// <param name="mediaTypeFormatters">Expected collection of media type formatters provided as parameters.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingMediaTypeFormatters(params MediaTypeFormatter[] mediaTypeFormatters);

        /// <summary>
        /// Tests whether created result contains the media type formatters provided by builder.
        /// </summary>
        /// <param name="formattersBuilder">Builder for expected media type formatters.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder ContainingMediaTypeFormatters(Action<IFormattersBuilder> formattersBuilder);
    }
}
