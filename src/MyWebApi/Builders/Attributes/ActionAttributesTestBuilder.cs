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

namespace MyWebApi.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Contracts.Attributes;
    using Common.Extensions;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing attributes.
    /// </summary>
    public class ActionAttributesTestBuilder : BaseAttributesTestBuilder, IAndActionAttributesTestBuilder
    {
        private readonly string actionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAttributesTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the attributes will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ActionAttributesTestBuilder(ApiController controller, string actionName)
            : base(controller)
        {
            this.actionName = actionName;
        }

        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain ActionNameAttribute.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName)
        {
            this.ContainingAttributeOfType<ActionNameAttribute>();
            this.Validations.Add(attrs =>
            {
                var actionNameAttribute = this.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;
                if (actionName != actualActionName)
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} with '{1}' name", actionNameAttribute.GetName(), actionName),
                        string.Format("in fact found '{0}'", actualActionName));
                }
            });

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null,
            string withAllowedUsers = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles,
                withAllowedUsers);

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain NonActionAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder DisablingActionCall()
        {
            return this.ContainingAttributeOfType<NonActionAttribute>();
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <typeparam name="THttpMethod">Attribute of type IActionHttpMethodProvider.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
        {
            return this.RestrictingForRequestsWithMethods((new THttpMethod()).HttpMethods);
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as string.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod)
        {
            return this.RestrictingForRequestsWithMethod(new HttpMethod(httpMethod));
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP method provided as HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod)
        {
            return this.RestrictingForRequestsWithMethods(new List<HttpMethod> { httpMethod });
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of strings.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.Select(m => new HttpMethod(m)));
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as string parameters.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as collection of HttpMethod classes.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods)
        {
            this.Validations.Add(attrs =>
            {
                var totalAllowedHttpMethods = attrs.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

                httpMethods.ForEach(httpMethod =>
                {
                    if (!totalAllowedHttpMethods.Contains(httpMethod))
                    {
                        this.ThrowNewAttributeAssertionException(
                            string.Format("attribute restricting requests for HTTP '{0}' method", httpMethod.Method),
                            "in fact none was found");
                    }
                });
            });

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP methods provided as parameters of HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        /// <summary>
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IActionAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                        "When calling {0} action in {1} expected action to have {2}, but {3}.",
                        this.actionName,
                        this.Controller.GetName(),
                        expectedValue,
                        actualValue));
        }
    }
}
