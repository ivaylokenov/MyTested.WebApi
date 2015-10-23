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

namespace MyWebApi.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using RouteResolvers;

    public static class RouteValidator
    {
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
