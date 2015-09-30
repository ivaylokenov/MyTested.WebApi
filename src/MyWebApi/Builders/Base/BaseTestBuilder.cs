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

namespace MyWebApi.Builders.Base
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Base;
    using Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    public class BaseTestBuilder : IBaseTestBuilder
    {
        private ApiController controller;
        private string actionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilder(
            ApiController controller,
            string actionName,
            IEnumerable<object> actionAttributes = null)
        {
            this.Controller = controller;
            this.ActionName = actionName;
            this.ActionLevelAttributes = actionAttributes;
        }

        /// <summary>
        /// Gets the controller on which the action will be tested.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        internal ApiController Controller
        {
            get
            {
                return this.controller;
            }

            private set
            {
                CommonValidator.CheckForNullReference(value, errorMessageName: "Controller");
                this.controller = value;
            }
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        internal string ActionName
        {
            get
            {
                return this.actionName;
            }

            private set
            {
                CommonValidator.CheckForNotWhiteSpaceString(value, errorMessageName: "ActionName");
                this.actionName = value;
            }
        }

        internal IEnumerable<object> ActionLevelAttributes { get; private set; }

        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET Web API controller on which the action is tested.</returns>
        public ApiController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <returns>Action name to be tested.</returns>
        public string AndProvideTheActionName()
        {
            return this.ActionName;
        }

        /// <summary>
        /// Gets the action attributes on the called action.
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes are found on the action.</returns>
        public IEnumerable<object> AndProvideTheActionAttributes()
        {
            return this.ActionLevelAttributes;
        }

        /// <summary>
        /// Gets the HTTP request message with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequestMessage from the tested controller.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.Controller.Request;
        }

        /// <summary>
        /// Checks whether the tested action's model state is valid.
        /// </summary>
        protected void CheckValidModelState()
        {
            if (!this.controller.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
        }
    }
}
