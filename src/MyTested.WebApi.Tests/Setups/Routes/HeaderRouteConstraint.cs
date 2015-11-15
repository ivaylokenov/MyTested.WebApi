// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Routes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class HeaderRouteConstraint : IHttpRouteConstraint
    {
        private readonly string name;
        private readonly string value;

        public HeaderRouteConstraint(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public bool Match(
            HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            var headers = request.Headers;
            var contentHeaders = request.Content != null ? request.Content.Headers : null;

            return (headers.Contains(this.name) && headers.GetValues(this.name).Contains(this.value))
                || (contentHeaders != null && contentHeaders.Contains(this.name) && contentHeaders.GetValues(this.name).Contains(this.value));
        }
    }
}
