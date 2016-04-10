// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Routes
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public interface IShouldMapTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// Adds HTTP method to the built route test.
        /// </summary>
        /// <param name="httpMethod">HTTP method represented by string.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithHttpMethod(string httpMethod);

        /// <summary>
        /// Adds HTTP method to the built route test.
        /// </summary>
        /// <param name="httpMethod">HTTP method represented by HttpMethod.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod);

        /// <summary>
        /// Add HTTP header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithRequestHeader(string name, string value);

        /// <summary>
        /// Add HTTP header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithRequestHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds collection of HTTP headers to the built route test.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Add HTTP content header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the content header.</param>
        /// <param name="value">Value of the content header.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithContentHeader(string name, string value);

        /// <summary>
        /// Add HTTP content header to the built route test. 
        /// </summary>
        /// <param name="name">Name of the content header.</param>
        /// <param name="values">Collection of values for the content header.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithContentHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds collection of HTTP content headers to the built route test.
        /// </summary>
        /// <param name="headers">Dictionary of content headers to add.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Adds URL encoded content to the built route test.
        /// </summary>
        /// <param name="content">URL encoded content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithFormUrlEncodedContent(string content);

        /// <summary>
        /// Adds JSON content to the built route test.
        /// </summary>
        /// <param name="content">JSON content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithJsonContent(string content);

        /// <summary>
        /// Adds content to the built route test.
        /// </summary>
        /// <param name="content">Content represented by string.</param>
        /// <param name="mediaType">Media type of the content represented by string.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithContent(string content, string mediaType);

        /// <summary>
        /// Adds content to the built route test.
        /// </summary>
        /// <param name="content">Content represented by string.</param>
        /// <param name="mediaType">Media type of the content represented by MediaTypeHeaderValue.</param>
        /// <returns>The same route test builder.</returns>
        IAndShouldMapTestBuilder WithContent(string content, MediaTypeHeaderValue mediaType);

        /// <summary>
        /// Tests whether the built route is resolved to the provided action.
        /// </summary>
        /// <param name="action">Expected action name.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToAction(string action);

        /// <summary>
        /// Tests whether the built route is resolved to the provided controller name.
        /// </summary>
        /// <param name="controller">Expected controller name.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToController(string controller);

        /// <summary>
        /// Tests whether the built route is resolved to the provided controller type.
        /// </summary>
        /// <typeparam name="TController">Expected controller type.</typeparam>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder To<TController>()
            where TController : ApiController;

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;

        /// <summary>
        /// Tests whether the built route cannot be resolved because of not allowed method.
        /// </summary>
        void ToNotAllowedMethod();

        /// <summary>
        /// Tests whether the built route cannot be resolved.
        /// </summary>
        void ToNonExistingRoute();

        /// <summary>
        /// Tests whether the built route is ignored by StopRoutingHandler.
        /// </summary>
        void ToIgnoredRoute();
    }
}
