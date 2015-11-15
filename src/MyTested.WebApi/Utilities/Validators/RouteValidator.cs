// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using RouteResolvers;

    /// <summary>
    /// Validator class containing route URI validator.
    /// </summary>
    public static class RouteValidator
    {
        /// <summary>
        /// Validates whether route generated URI is the same as action call generated URI.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="request">HttpRequestMessage to use for generating the route URI.</param>
        /// <param name="routeName">Route name from the HTTP configuration routes.</param>
        /// <param name="routeValues">Route values dictionary of string-object pairs.</param>
        /// <param name="expectedActionCall">Method call expression indicating the expected action.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void Validate<TController>(
            HttpRequestMessage request,
            string routeName,
            IDictionary<string, object> routeValues,
            LambdaExpression expectedActionCall,
            Action<string, string, string> failedValidationAction)
            where TController : ApiController
        {
            var urlHelper = new UrlHelper(request);
            var actualUri = urlHelper.Route(routeName, routeValues);

            var expectedRouteValues = RouteExpressionParser.Parse<TController>(expectedActionCall).ToRouteValueDictionary();
            var expectedUri = urlHelper.Route(routeName, expectedRouteValues);
            if (expectedUri == null)
            {
                failedValidationAction(
                    "to redirect to",
                    "a specific URI",
                    string.Format("such URI could not be resolved from the '{0}' route template", routeName));
            }

            if (actualUri != expectedUri)
            {
                failedValidationAction(
                    "to redirect to",
                    string.Format("'{0}'", expectedUri),
                    string.Format("in fact redirected to '{0}'", actualUri));
            }
        }
    }
}
