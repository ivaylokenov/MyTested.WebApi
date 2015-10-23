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

namespace MyWebApi.Common.Routes
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

        public IDictionary<string, object> ToRouteValueDictionary()
        {
            var result = Arguments.ToDictionary(a => a.Key, a => a.Value.Value);
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
