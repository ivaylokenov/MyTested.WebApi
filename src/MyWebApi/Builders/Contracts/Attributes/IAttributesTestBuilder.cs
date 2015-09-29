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

namespace MyWebApi.Builders.Contracts.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Controllers;

    public interface IAttributesTestBuilder
    {
        IAndAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute;

        IAndAttributesTestBuilder ChangingActionNameTo(string actionName);

        IAndAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null);

        IAndAttributesTestBuilder AllowingAnonymousRequests();

        IAndAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null,
            string withAllowedUsers = null);

        IAndAttributesTestBuilder DisablingActionCall();

        IAndAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new();

        IAndAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod);

        IAndAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod);

        IAndAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods);

        IAndAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods);

        IAndAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods);

        IAndAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods);
    }
}
