// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Routes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    /// <summary>
    /// Contains route information from parsed expression.
    /// </summary>
    public class ExpressionParsedRouteInfo
    {
        private const string ControllerName = "Controller";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsedRouteInfo" /> class.
        /// </summary>
        /// <param name="controller">Controller type.</param>
        /// <param name="action">Action name.</param>
        /// <param name="arguments">IEnumerable of action arguments.</param>
        public ExpressionParsedRouteInfo(
            Type controller,
            string action,
            IEnumerable<MethodArgumentInfo> arguments)
        {
            this.Controller = controller;
            this.Action = action;
            this.Arguments = arguments.ToDictionary(a => a.Name);
        }

        /// <summary>
        /// Gets the controller type from the parsed expression.
        /// </summary>
        /// <value>The controller type.</value>
        public Type Controller { get; private set; }

        /// <summary>
        /// Gets the action name from the parsed expression.
        /// </summary>
        /// <value>The action type.</value>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the action arguments the parsed expression.
        /// </summary>
        /// <value>Dictionary of string-argument info pairs.</value>
        public IDictionary<string, MethodArgumentInfo> Arguments { get; private set; }

        /// <summary>
        /// Converts the parsed information to route value dictionary.
        /// </summary>
        /// <returns>Dictionary of string-object pairs.</returns>
        public IDictionary<string, object> ToRouteValueDictionary()
        {
            var result = this.Arguments.ToDictionary(a => a.Key, a => a.Value.Value);
            result.Add("controller", this.GetControllerName());
            result.Add("action", this.Action);
            return result;
        }

        private string GetControllerName()
        {
            var controllerName = this.Controller.Name;
            if ((controllerName.Length > ControllerName.Length) && controllerName.EndsWith(ControllerName))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - ControllerName.Length);
            }

            return controllerName;
        }
    }
}
