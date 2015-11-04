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

namespace MyWebApi
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Handlers;
    using Builders.Contracts.Routes;
    using Builders.Controllers;
    using Builders.HttpMessages;
    using Builders.Routes;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Starting point of the testing framework, which provides a way to specify the ASP.NET Web API feature to be tested.
    /// </summary>
    public static class MyWebApi
    {
        /// <summary>
        /// Gets the current global HTTP configuration used in the testing.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        public static HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// Sets the HttpConfiguration which will be used in all tests.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration instance used in the testing.</param>
        public static void IsUsing(HttpConfiguration httpConfiguration)
        {
            Configuration = httpConfiguration;
        }

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <param name="httpConfiguration">Optional HttpConfiguration to use in case one is not configured globally.</param>
        /// <returns>Route test builder.</returns>
        public static IRouteTestBuilder Routes(HttpConfiguration httpConfiguration = null)
        {
            if (httpConfiguration == null)
            {
                CommonValidator.CheckForNullReference(
                    Configuration,
                    "'IsUsing' method should be called before testing routes or HttpConfiguration should be provided. MyWebApi must be configured and HttpConfiguration");

                httpConfiguration = Configuration;
            }

            return new RouteTestBuilder(httpConfiguration);
        }

        public static IHttpMessageHandlerBuilder Handler<THandler>()
            where THandler : HttpMessageHandler
        {
            var handler = Reflection.TryCreateInstance<THandler>();
            return Handler(() => handler);
        }

        public static IHttpMessageHandlerBuilder Handler(HttpMessageHandler handler)
        {
            return Handler(() => handler);
        }

        public static IHttpMessageHandlerBuilder Handler(Func<HttpMessageHandler> handler)
        {
            var handlerInstance = handler();
            return new HttpMessageHandlerTestBuilder(handlerInstance);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controller = Reflection.TryCreateInstance<TController>();
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET Web API controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : ApiController
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }
    }
}
