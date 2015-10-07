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
    using System.Net.Http;
    using System.Web.Http;
    using Contracts.Base;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    public class BaseTestBuilder : IBaseTestBuilder
    {
        private ApiController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        protected BaseTestBuilder(
            ApiController controller)
        {
            this.Controller = controller;
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
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET Web API controller on which the action is tested.</returns>
        public ApiController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Gets the HTTP request message with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequestMessage from the tested controller.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.Controller.Request;
        }
    }
}
