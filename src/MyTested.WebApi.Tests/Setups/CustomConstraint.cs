namespace MyTested.WebApi.Tests.Setups
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class CustomConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
            => true;
    }
}
