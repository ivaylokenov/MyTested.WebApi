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

namespace MyWebApi.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Base;
    using Common.Extensions;
    using Contracts.Attributes;
    using Exceptions;
    using Utilities;

    public class AttributesTestBuilder : BaseTestBuilder, IAndAttributesTestBuilder
    {
        private readonly ICollection<Action<IEnumerable<object>>> validations;

        public AttributesTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
            this.validations = new List<Action<IEnumerable<object>>>();
        }

        public IAndAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            var expectedAttributeType = typeof(TAttribute);
            this.validations.Add(attrs =>
            {
                if (attrs.All(a => a.GetType() != expectedAttributeType))
                {
                    this.ThrowNewAttributeAssertionException(
                        expectedAttributeType.ToFriendlyTypeName(),
                        "in fact none was found");
                }
            });

            return this;
        }
        
        public IAndAttributesTestBuilder ChangingActionNameTo(string actionName)
        {
            this.ContainingAttributeOfType<ActionNameAttribute>();
            this.validations.Add(attrs =>
            {
                var actionNameAttribute = this.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;
                if (actionName != actualActionName)
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} '{1}'", actionNameAttribute.GetName(), actionName),
                        string.Format("in fact found '{0}'", actualActionName));
                }
            });

            return this;
        }

        public IAndAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ContainingAttributeOfType<RouteAttribute>();
            this.validations.Add(attrs =>
            {
                var routeAttribute = this.TryGetAttributeOfType<RouteAttribute>(attrs);
                var actualTemplate = routeAttribute.Template;
                if (template != actualTemplate)
                {
                    this.ThrowNewAttributeAssertionException(
                                string.Format("{0} with '{1}' template", routeAttribute.GetName(), template),
                                string.Format("in fact found '{0}'", actualTemplate));
                }

                var actualName = routeAttribute.Name;
                if (!string.IsNullOrEmpty(withName) && withName != actualName)
                {
                    this.ThrowNewAttributeAssertionException(
                                string.Format("{0} with '{1}' name", routeAttribute.GetName(), withName),
                                string.Format("in fact found '{0}'", actualName));
                }

                var actualOrder = routeAttribute.Order;
                if (withOrder.HasValue && withOrder != actualOrder)
                {
                    this.ThrowNewAttributeAssertionException(
                                string.Format("{0} with order of {1}", routeAttribute.GetName(), withOrder),
                                string.Format("in fact found {0}", actualOrder));
                }
            });

            return this;
        }

        public IAndAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        public IAndAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedUsers = null,
            string withAllowedRoles = null)
        {
            this.ContainingAttributeOfType<AuthorizeAttribute>();
            var testAllowedUsers = !string.IsNullOrEmpty(withAllowedUsers);
            var testAllowedRoles = !string.IsNullOrEmpty(withAllowedRoles);
            if (testAllowedUsers || testAllowedRoles)
            {
                if (testAllowedUsers)
                {
                    this.validations.Add(attrs =>
                    {
                        var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                        var actualUsers = authorizeAttribute.Users;
                        if (withAllowedUsers != actualUsers)
                        {
                            this.ThrowNewAttributeAssertionException(
                                string.Format("{0} with allowed '{1}' users", authorizeAttribute.GetName(), withAllowedUsers),
                                string.Format("in fact found '{0}'", actualUsers));
                        }
                    });
                }

                if (testAllowedRoles)
                {
                    this.validations.Add(attrs =>
                    {
                        var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                        var actualRoles = authorizeAttribute.Roles;
                        if (withAllowedRoles != actualRoles)
                        {
                            this.ThrowNewAttributeAssertionException(
                                string.Format("{0} with allowed '{1}' roles", authorizeAttribute.GetName(), withAllowedRoles),
                                string.Format("in fact found '{0}'", actualRoles));
                        }
                    });
                }
            }

            return this;
        }

        public IAndAttributesTestBuilder DisablingAction()
        {
            return this.ContainingAttributeOfType<NonActionAttribute>();
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
        {
            return this.RestrictingForRequestsWithMethods((new THttpMethod()).HttpMethods);
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod)
        {
            return this.RestrictingForRequestsWithMethod(new HttpMethod(httpMethod));
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod)
        {
            return this.RestrictingForRequestsWithMethods(new List<HttpMethod> { httpMethod });
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.Select(m => new HttpMethod(m)));
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods)
        {
            this.validations.Add(attrs =>
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

        public IAndAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        public IAttributesTestBuilder AndAlso()
        {
            return this;
        }

        internal ICollection<Action<IEnumerable<object>>> GetAttributeValidations()
        {
            return this.validations;
        }

        private TAttribute GetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return (TAttribute)attributes.First(a => a.GetType() == typeof(TAttribute));
        }

        private TAttribute TryGetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return attributes.FirstOrDefault(a => a.GetType() == typeof(TAttribute)) as TAttribute;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                        "When calling {0} action in {1} expected action to have {2}, but {3}.",
                        this.Controller.GetName(),
                        this.ActionName,
                        expectedValue,
                        actualValue));
        }
    }
}
