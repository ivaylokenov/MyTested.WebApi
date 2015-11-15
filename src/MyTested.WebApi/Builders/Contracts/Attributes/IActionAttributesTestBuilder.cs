// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public interface IActionAttributesTestBuilder
    {
        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;

        /// <summary>
        /// Checks whether the collected attributes contain ActionNameAttribute.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName);

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null);

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder AllowingAnonymousRequests();

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null,
            string withAllowedUsers = null);

        /// <summary>
        /// Checks whether the collected attributes contain NonActionAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder DisablingActionCall();

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <typeparam name="THttpMethod">Attribute of type IActionHttpMethodProvider.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new();

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as string.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod);

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod);

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of strings.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods);

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as string parameters.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods);

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of HttpMethod classes.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods);

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as parameters of HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods);
    }
}
