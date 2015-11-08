// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Base
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using Contracts.Base;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    public abstract class BaseTestBuilder : IBaseTestBuilder
    {
        private ApiController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which will be tested.</param>
        /// <param name="controllerAttributes">Collected attributes from the tested controller.</param>
        protected BaseTestBuilder(
            ApiController controller,
            IEnumerable<object> controllerAttributes = null)
        {
            this.Controller = controller;
            this.ControllerLevelAttributes = controllerAttributes;
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

        internal IEnumerable<object> ControllerLevelAttributes { get; private set; }

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

        /// <summary>
        /// Gets the HTTP configuration with which the action will be tested.
        /// </summary>
        /// <returns>HttpConfiguration from the tested controller.</returns>
        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.Controller.Configuration;
        }

        /// <summary>
        /// Gets the attributes on the tested controller..
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the controller.</returns>
        public IEnumerable<object> AndProvideTheControllerAttributes()
        {
            return this.ControllerLevelAttributes;
        }
    }
}
