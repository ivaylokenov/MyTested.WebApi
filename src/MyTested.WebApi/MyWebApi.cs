// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Builders;
    using Builders.Contracts;
    using Builders.Contracts.Servers;
    using Builders.Controllers;
    using Builders.HttpMessages;
    using Builders.Routes;
    using Builders.Servers;
    using Common.Extensions;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Starting point of the testing framework, which provides a way to specify the ASP.NET Web API feature to be tested.
    /// </summary>
    public static class MyWebApi
    {
        /// <summary>
        /// Default host which the tests will use - local host.
        /// </summary>
        public const string DefaultHost = "http://localhost";

        /// <summary>
        /// Default port which the tests will use - 80.
        /// </summary>
        public const int DefaultPort = 80;

        static MyWebApi()
        {
            try
            {
                IsUsingDefaultHttpConfiguration();
            }
            catch (InvalidOperationException)
            {
                // This exception can be swallowed - arises when a custom route constraint cannot
                // be resolved by the DefaultInlineConstraintResolver.
                // See https://github.com/ivaylokenov/MyTested.WebApi/issues/270 for more info.
            }
        }

        /// <summary>
        /// Gets the current global HTTP configuration used in the testing.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        public static HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets the current base address used in the testing.
        /// </summary>
        /// <value>Instance of String.</value>
        public static Uri BaseAddress { get; internal set; }

        /// <summary>
        /// Sets the default HttpConfiguration which will be used in all tests.
        /// </summary>
        /// <param name="constraintResolver">
        /// The optional constraint Resolver.
        /// </param>
        /// <returns>
        /// HTTP configuration builder.
        /// </returns>
        public static IHttpConfigurationBuilder IsUsingDefaultHttpConfiguration(
            IInlineConstraintResolver constraintResolver = null
        )
        {
            var config = new HttpConfiguration();

            if (constraintResolver == null)
            {
                config.MapHttpAttributeRoutes();
            }
            else
            {
                config.MapHttpAttributeRoutes(constraintResolver);
            }

            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            return IsUsing(config);
        }

        /// <summary>
        /// Sets the HttpConfiguration which will be used in all tests.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration instance used in the testing.</param>
        /// <returns>HTTP configuration builder.</returns>
        public static IHttpConfigurationBuilder IsUsing(HttpConfiguration httpConfiguration)
        {
            Configuration = httpConfiguration;
            BaseAddress = new Uri(DefaultHost, UriKind.Absolute);
            return new HttpConfigurationBuilder(httpConfiguration);
        }

        /// <summary>
        /// Sets the action which will register the HttpConfiguration used in all tests.
        /// </summary>
        /// <param name="httpConfigurationRegistration">Action to register HttpConfiguration.</param>
        /// <returns>HTTP configuration builder.</returns>
        public static IHttpConfigurationBuilder IsRegisteredWith(Action<HttpConfiguration> httpConfigurationRegistration)
        {
            var configuration = new HttpConfiguration();
            httpConfigurationRegistration(configuration);
            return IsUsing(configuration);
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
                HttpConfigurationValidator.ValidateGlobalConfiguration("routes");
                httpConfiguration = Configuration;
            }

            return new RouteTestBuilder(httpConfiguration);
        }

        /// <summary>
        /// Selects HTTP message handler on which the test will be executed. HttpMessageHandler is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="THandler">Instance of type HttpMessageHandler.</typeparam>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler<THandler>()
            where THandler : HttpMessageHandler
        {
            var handler = Reflection.TryCreateInstance<THandler>();
            return Handler(() => handler);
        }

        /// <summary>
        /// Selects HTTP message handler on which the test will be executed.
        /// </summary>
        /// <param name="handler">Instance of the HttpMessageHandler to use.</param>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler(HttpMessageHandler handler)
        {
            return Handler(() => handler);
        }

        /// <summary>
        /// Selects HTTP message handler on which the test will be executed. HttpMessageHandler is instantiated using construction function.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated HttpMessageHandler.</param>
        /// <returns>Handler builder used to build the test case.</returns>
        public static IHttpMessageHandlerBuilder Handler(Func<HttpMessageHandler> construction)
        {
            var handlerInstance = construction();
            return new HttpMessageHandlerTestBuilder(handlerInstance);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with the globally registered dependency resolver or with its default constructor, if resolving is unsuccessful.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controller = Configuration.TryResolve<TController>() ?? Reflection.TryFastCreateInstance<TController>();
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

        /// <summary>
        /// Starts a full ASP.NET Web API pipeline test.
        /// </summary>
        /// <returns>Server instance to set the HTTP request and test the HTTP response.</returns>
        public static IServer Server()
        {
            return new Server();
        }
    }
}
