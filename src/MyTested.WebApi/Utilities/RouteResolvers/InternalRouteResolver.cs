// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.RouteResolvers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;
    using Common.Extensions;
    using Common.Routes;
    using MyTested.WebApi.Exceptions;

    /// <summary>
    /// Used for resolving HTTP request message to a route.
    /// </summary>
    public static class InternalRouteResolver
    {
        private const string UnresolvedRouteFormat = "it could not be resolved: '{0}'";

        /// <summary>
        /// Resolves HTTP request message to a route using the provided HTTP configuration and the internal ASP.NET Web API route resolving algorithm.
        /// </summary>
        /// <param name="config">HttpConfiguration to use.</param>
        /// <param name="request">HttpRequestMessage to resolve.</param>
        /// <returns>Resolved route information.</returns>
        public static ResolvedRouteInfo Resolve(HttpConfiguration config, HttpRequestMessage request)
        {
            try
            {
                config.EnsureInitialized();
            }
            catch (InvalidOperationException ex)
            when (ex.IsRouteConstraintRelatedException())
            {
                throw new UnresolvedRouteConstraintsException(@"An error occurred while resolving your routes. If you are using custom route constraints, 
                    please call WithInlineConstgraintResolver method with appropriate IInlineConstraintResolver instance after httpConfiguration setup.");
            }

            // transform the URI to fake absolute one since ASP.NET Web API internal route resolver does no support non-absolute URIs
            var originalRoute = request.RequestUri;
            request.TransformToAbsoluteRequestUri();

            var routeData = config.Routes.GetRouteData(request);
            if (routeData == null)
            {
                request.RequestUri = originalRoute;
                return new ResolvedRouteInfo("route does not exist");
            }

            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            routeData.RemoveOptionalRoutingParameters();

            ResolvedRouteInfo resolvedRouteInfo;
            try
            {
                var controllerContext = GetHttpControllerContext(config, request, routeData);
                var actionContext = GetHttpActionContext(config, controllerContext);
                resolvedRouteInfo = new ResolvedRouteInfo(
                    controllerContext.ControllerDescriptor.ControllerType,
                    actionContext.ActionDescriptor.ActionName,
                    actionContext.ActionArguments,
                    routeData.Route.Handler,
                    actionContext.ModelState);
            }
            catch (HttpResponseException ex)
            {
                resolvedRouteInfo = new ResolvedRouteInfo(string.Format(
                    UnresolvedRouteFormat,
                    ex.Response.ReasonPhrase));
            }
            catch (AggregateException ex)
            {
                var innerException = (HttpResponseException)ex.InnerExceptions.First();
                resolvedRouteInfo = new ResolvedRouteInfo(string.Format(
                    UnresolvedRouteFormat,
                    innerException.Response.ReasonPhrase));
            }
            catch (Exception ex)
            {
                resolvedRouteInfo = new ResolvedRouteInfo(string.Format(
                    UnresolvedRouteFormat,
                    ex.Message.Split(':').First()));
            }

            request.RequestUri = originalRoute;
            return resolvedRouteInfo;
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
