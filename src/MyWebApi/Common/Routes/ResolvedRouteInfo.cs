// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Common.Routes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Routing;

    /// <summary>
    /// Contains information about a resolved route from the ASP.NET Web API internal pipeline.
    /// </summary>
    public class ResolvedRouteInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedRouteInfo" /> class.
        /// </summary>
        /// <param name="controller">Resolved controller type for the current route.</param>
        /// <param name="action">Resolved action name for the current route.</param>
        /// <param name="actionArguments">Resolved dictionary of the action arguments for the current route.</param>
        /// <param name="httpMessageHandler">Resolved HttpMessageHandler for the current route.</param>
        /// <param name="modelState">Resolved model state validation for the current route.</param>
        public ResolvedRouteInfo(
            Type controller,
            string action,
            IDictionary<string, object> actionArguments,
            HttpMessageHandler httpMessageHandler,
            ModelStateDictionary modelState)
        {
            this.IsResolved = true;
            this.Controller = controller;
            this.Action = action;
            this.HttpMessageHandler = httpMessageHandler;
            this.ModelState = modelState;
            this.ActionArguments = actionArguments.ToDictionary(
                a => a.Key,
                a => new MethodArgumentInfo
                {
                    Name = a.Key,
                    Type = a.Value != null ? a.Value.GetType() : null,
                    Value = a.Value
                });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedRouteInfo" /> class.
        /// </summary>
        /// <param name="unresolvedError">Received error during the route resolving.</param>
        public ResolvedRouteInfo(string unresolvedError)
        {
            this.IsResolved = false;
            this.UnresolvedError = unresolvedError;
        }

        /// <summary>
        /// Gets a value indicating whether the current route is successfully resolved.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsResolved { get; private set; }

        /// <summary>
        /// Gets route error in case of unsuccessful resolving.
        /// </summary>
        /// <value>The error as string or null, if the route was resolved successfully.</value>
        public string UnresolvedError { get; private set; }

        /// <summary>
        /// Gets the resolved controller type for the current route.
        /// </summary>
        /// <value>Type of the controller.</value>
        public Type Controller { get; private set; }

        /// <summary>
        /// Gets the resolved action name for the current route.
        /// </summary>
        /// <value>The action name as string.</value>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the resolved action arguments for the current route.
        /// </summary>
        /// <value>Dictionary of string-argument pairs.</value>
        public IDictionary<string, MethodArgumentInfo> ActionArguments { get; private set; }

        /// <summary>
        /// Gets the resolved HttpMessageHandler for the current route.
        /// </summary>
        /// <value>Instance of HttpMessageHandler.</value>
        public HttpMessageHandler HttpMessageHandler { get; private set; }

        /// <summary>
        /// Gets the resolved model state validation for the current route.
        /// </summary>
        /// <value>Instance of ModelStateDictionary.</value>
        public ModelStateDictionary ModelState { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current route is ignored with StopRoutingHandler.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsIgnored
        {
            get { return this.HttpMessageHandler is StopRoutingHandler; }
        }

        /// <summary>
        /// Gets a value indicating whether the current route is not resolved because of not allowed method.
        /// </summary>
        /// <value>True or false.</value>
        public bool MethodIsNotAllowed
        {
            get { return this.UnresolvedError != null && this.UnresolvedError.Contains("Method Not Allowed"); }
        }
    }
}
