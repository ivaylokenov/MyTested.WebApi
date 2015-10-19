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

namespace MyWebApi.Utilities.RouteResolvers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;
    using Common;

    public static class InternalRouteResolver
    {
        private const string UnresolvedRouteFormat = "Route '{0}' could not be resolved: '{1}'.";

        public static ResolvedRouteInfo Resolve(HttpConfiguration config, HttpRequestMessage request)
        {
            config.EnsureInitialized();

            // transform the URI to fake absolute one since ASP.NET Web API internal route resolver does no support non-absolute URIs
            var originalRoute = request.RequestUri;
            request.RequestUri = new Uri(new Uri("http://absoluteuri.com"), request.RequestUri);

            var routeData = config.Routes.GetRouteData(request);

            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            routeData.RemoveOptionalRoutingParameters();

            HttpControllerContext controllerContext;
            HttpActionContext actionContext;
            try
            {
                controllerContext = GetHttpControllerContext(config, request, routeData);
                actionContext = GetHttpActionContext(config, controllerContext);
            }
            catch (HttpResponseException ex)
            {
                return new ResolvedRouteInfo(string.Format(
                    UnresolvedRouteFormat,
                    originalRoute,
                    ex.Response.ReasonPhrase));
            }
            catch (Exception ex)
            {
                return new ResolvedRouteInfo(string.Format(
                    UnresolvedRouteFormat,
                    originalRoute,
                    ex.Message.Split(':').First()));
            }

            return new ResolvedRouteInfo(
                controllerContext.ControllerDescriptor.ControllerName,
                actionContext.ActionDescriptor.ActionName,
                actionContext.ActionArguments,
                routeData.Route.Handler,
                actionContext.ModelState);
        }

        private static HttpControllerContext GetHttpControllerContext(
            HttpConfiguration config,
            HttpRequestMessage request,
            IHttpRouteData routeData)
        {
            var controllerSelector = config.Services.GetHttpControllerSelector();
            var controllerDescriptor = controllerSelector.SelectController(request);

            return new HttpControllerContext(config, routeData, request)
            {
                ControllerDescriptor = controllerDescriptor
            };
        }

        private static HttpActionContext GetHttpActionContext(
            HttpConfiguration config,
            HttpControllerContext controllerContext)
        {
            var actionSelector = config.Services.GetActionSelector();
            var actionDescriptor = actionSelector.SelectAction(controllerContext);
            var actionContext = new HttpActionContext(controllerContext, actionDescriptor);
            actionDescriptor.ActionBinding.ExecuteBindingAsync(actionContext, CancellationToken.None).Wait();

            return actionContext;
        }
    }
}
