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
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Utilities;

    public abstract class BaseAttributesTestBuilder : BaseTestBuilder
    {
        protected BaseAttributesTestBuilder(ApiController controller)
            : base(controller)
        {
            this.Validations = new List<Action<IEnumerable<object>>>();
        }

        protected ICollection<Action<IEnumerable<object>>> Validations { get; private set; }

        internal ICollection<Action<IEnumerable<object>>> GetAttributeValidations()
        {
            return this.Validations;
        }

        protected void ContainingAttributeOfType<TAttribute>(Action<string, string> failedValidationAction)
            where TAttribute : Attribute
        {
            var expectedAttributeType = typeof(TAttribute);
            this.Validations.Add(attrs =>
            {
                if (attrs.All(a => a.GetType() != expectedAttributeType))
                {
                    failedValidationAction(
                        expectedAttributeType.ToFriendlyTypeName(),
                        "in fact such was not found");
                }
            });
        }

        protected void ChangingRouteTo(
            string template,
            Action<string, string> failedValidationAction,
            string withName = null,
            int? withOrder = null)
        {
            this.ContainingAttributeOfType<RouteAttribute>(failedValidationAction);
            this.Validations.Add(attrs =>
            {
                var routeAttribute = this.TryGetAttributeOfType<RouteAttribute>(attrs);
                var actualTemplate = routeAttribute.Template;
                if (template != actualTemplate)
                {
                    failedValidationAction(
                                string.Format("{0} with '{1}' template", routeAttribute.GetName(), template),
                                string.Format("in fact found '{0}'", actualTemplate));
                }

                var actualName = routeAttribute.Name;
                if (!string.IsNullOrEmpty(withName) && withName != actualName)
                {
                    failedValidationAction(
                                string.Format("{0} with '{1}' name", routeAttribute.GetName(), withName),
                                string.Format("in fact found '{0}'", actualName));
                }

                var actualOrder = routeAttribute.Order;
                if (withOrder.HasValue && withOrder != actualOrder)
                {
                    failedValidationAction(
                                string.Format("{0} with order of {1}", routeAttribute.GetName(), withOrder),
                                string.Format("in fact found {0}", actualOrder));
                }
            });
        }

        protected TAttribute GetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return (TAttribute)attributes.First(a => a.GetType() == typeof(TAttribute));
        }

        protected TAttribute TryGetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return attributes.FirstOrDefault(a => a.GetType() == typeof(TAttribute)) as TAttribute;
        }
    }
}
