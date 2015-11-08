// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Attributes
{
    using System;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Attributes;
    using Exceptions;

    /// <summary>
    /// Used for testing controller attributes.
    /// </summary>
    public class ControllerAttributesTestBuilder : BaseAttributesTestBuilder, IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerAttributesTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller which will be tested.</param>
        public ControllerAttributesTestBuilder(ApiController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ChangingRouteTo(
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
        /// Checks whether the collected attributes contain RoutePrefixAttribute.
        /// </summary>
        /// <param name="prefix">Expected overridden route prefix of the controller.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ChangingRoutePrefixTo(string prefix)
        {
            this.ContainingAttributeOfType<RoutePrefixAttribute>();
            this.Validations.Add(attrs =>
            {
                var routePrefixAttribute = this.GetAttributeOfType<RoutePrefixAttribute>(attrs);
                var actualRoutePrefix = routePrefixAttribute.Prefix;
                if (prefix != actualRoutePrefix)
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} with '{1}' prefix", routePrefixAttribute.GetName(), prefix),
                        string.Format("in fact found '{0}'", actualRoutePrefix));
                }
            });

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder RestrictingForAuthorizedRequests(
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
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IControllerAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
