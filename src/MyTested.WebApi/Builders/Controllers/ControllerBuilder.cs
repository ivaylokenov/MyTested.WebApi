// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Actions;
    using Common;
    using Common.Extensions;
    using Contracts;
    using Contracts.Actions;
    using Contracts.Controllers;
    using Contracts.HttpRequests;
    using Contracts.Identity;
    using Exceptions;
    using HttpMessages;
    using Identity;
    using Utilities;

    /// <summary>
    /// Used for building the action which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public class ControllerBuilder<TController> : IAndControllerBuilder<TController>
        where TController : ApiController
    {
        private readonly IDictionary<Type, object> aggregatedDependencies;

        private TController controller;
        private bool isPreparedForTesting;
        private bool enabledValidation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}" /> class.
        /// </summary>
        /// <param name="controllerInstance">Instance of the tested ASP.NET Web API controller.</param>
        public ControllerBuilder(TController controllerInstance)
        {
            this.Controller = controllerInstance;
            this.aggregatedDependencies = new Dictionary<Type, object>();
            this.HttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, MyWebApi.BaseAddress);
            this.isPreparedForTesting = false;
            this.enabledValidation = true;
        }

        /// <summary>
        /// Gets the ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET Web API controller.</value>
        protected TController Controller
        {
            get
            {
                this.BuildControllerIfNotExists();
                return this.controller;
            }

            private set
            {
                this.controller = value;
            }
        }

        /// <summary>
        /// Gets the HTTP request message used in the testing.
        /// </summary>
        /// <value>Instance of HttpRequestMessage.</value>
        protected HttpRequestMessage HttpRequestMessage { get; private set; }

        /// <summary>
        /// Gets the HTTP configuration used in the testing.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        protected HttpConfiguration HttpConfiguration { get; private set; }

        /// <summary>
        /// Sets the HTTP configuration for the current test case.
        /// </summary>
        /// <param name="config">Instance of HttpConfiguration.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpConfiguration(HttpConfiguration config)
        {
            this.HttpConfiguration = config;
            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested controller.
        /// </summary>
        /// <param name="requestMessage">Instance of HttpRequestMessage.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequestMessage(HttpRequestMessage requestMessage)
        {
            this.HttpRequestMessage = requestMessage;
            return this;
        }

        /// <summary>
        /// Adds HTTP request message to the tested controller.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">Builder for HTTP request message.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestMessageBuilder)
        {
            var httpBuilder = new HttpRequestMessageBuilder();
            httpRequestMessageBuilder(httpBuilder);
            return this.WithHttpRequestMessage(httpBuilder.GetHttpRequestMessage());
        }

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency)
        {
            var typeOfDependency = dependency != null
                ? dependency.GetType()
                : typeof(TDependency);

            if (this.aggregatedDependencies.ContainsKey(typeOfDependency))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} is already registered for {1} controller.",
                    typeOfDependency.ToFriendlyTypeName(),
                    typeof(TController).ToFriendlyTypeName()));
            }

            this.aggregatedDependencies.Add(typeOfDependency, dependency);
            this.controller = null;
            return this;
        }

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided collection of dependencies.
        /// </summary>
        /// <param name="dependencies">Collection of dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencies(IEnumerable<object> dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided dependencies.
        /// </summary>
        /// <param name="dependencies">Dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencies(params object[] dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }

        /// <summary>
        /// Disables ModelState validation for the action call.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithoutValidation()
        {
            this.enabledValidation = false;
            return this;
        }

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser()
        {
            this.Controller.User = MockedIPrincipal.CreateDefaultAuthenticated();
            return this;
        }

        /// <summary>
        /// Sets custom authenticated user to the built controller.
        /// </summary>
        /// <param name="pricipal">The IPrincipal user to set.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser(IPrincipal pricipal)
        {
            this.Controller.User = pricipal;
            return this;
        }

        /// <summary>
        /// Sets custom authenticated user using provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IUserBuilder> userBuilder)
        {
            var newUserBuilder = new UserBuilder();
            userBuilder(newUserBuilder);
            this.Controller.User = newUserBuilder.GetUser();
            return this;
        }

        /// <summary>
        /// Sets custom properties to the controller using action delegate.
        /// </summary>
        /// <param name="controllerSetup">Action delegate to use for controller setup.</param>
        /// <returns>The same controller test builder.</returns>
        public IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup)
        {
            controllerSetup(this.Controller);
            return this;
        }

        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        public IControllerTestBuilder ShouldHave()
        {
            var attributes = Reflection.GetCustomAttributes(this.Controller);
            return new ControllerTestBuilder(this.Controller, attributes);
        }

        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);
            return new ActionResultTestBuilder<TActionResult>(
                this.Controller,
                actionInfo.ActionName,
                actionInfo.CaughtException,
                actionInfo.ActionResult,
                actionInfo.ActionAttributes);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> CallingAsync<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);
            var actionResult = default(TActionResult);

            try
            {
                actionResult = actionInfo.ActionResult.Result;
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            return new ActionResultTestBuilder<TActionResult>(
                this.Controller,
                actionInfo.ActionName,
                actionInfo.CaughtException,
                actionResult,
                actionInfo.ActionAttributes);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            var actionAttributes = ExpressionParser.GetMethodAttributes(actionCall);
            Exception caughtException = null;

            try
            {
                actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return new VoidActionResultTestBuilder(this.Controller, actionName, caughtException, actionAttributes);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder CallingAsync(Expression<Func<TController, Task>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            try
            {
                actionInfo.ActionResult.Wait();
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            return new VoidActionResultTestBuilder(this.Controller, actionInfo.ActionName, actionInfo.CaughtException, actionInfo.ActionAttributes);
        }

        /// <summary>
        /// Gets ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <returns>Instance of the ASP.NET Web API controller.</returns>
        public TController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Gets the HTTP configuration used in the testing.
        /// </summary>
        /// <returns>Instance of HttpConfiguration.</returns>
        public HttpRequestMessage AndProvideTheHttpRequestMessage()
        {
            return this.HttpRequestMessage;
        }

        /// <summary>
        /// Gets the HTTP request message used in the testing.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        public HttpConfiguration AndProvideTheHttpConfiguration()
        {
            return this.Controller.Configuration;
        }

        private void BuildControllerIfNotExists()
        {
            if (this.controller == null)
            {
                if (this.aggregatedDependencies.Any())
                {
                    this.controller =
                        Reflection.TryCreateInstance<TController>(
                            this.aggregatedDependencies.Select(v => v.Value).ToArray());
                }
                else
                {
                    var configuration = this.HttpConfiguration ?? MyWebApi.Configuration;
                    if (configuration != null && configuration.DependencyResolver != null)
                    {
                        this.controller = configuration
                            .DependencyResolver
                            .BeginScope()
                            .GetService(typeof(TController)) as TController;
                    }
                }

                if (this.controller == null)
                {
                    var friendlyDependenciesNames = this.aggregatedDependencies
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyDependencies = string.Join(", ", friendlyDependenciesNames);

                    throw new UnresolvedDependenciesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TController).ToFriendlyTypeName(),
                        this.aggregatedDependencies.Count == 0 ? "no" : string.Format("{0} as", joinedFriendlyDependencies)));
                }
            }

            if (!this.isPreparedForTesting)
            {
                this.PrepareController();
                this.isPreparedForTesting = true;
            }
        }

        private ActionInfo<TActionResult> GetAndValidateActionResult<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            var actionResult = default(TActionResult);
            var actionAttributes = ExpressionParser.GetMethodAttributes(actionCall);
            Exception caughtException = null;

            try
            {
                actionResult = actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return new ActionInfo<TActionResult>(actionName, actionAttributes, actionResult, caughtException);
        }

        private string GetAndValidateAction(LambdaExpression actionCall)
        {
            if (this.enabledValidation)
            {
                this.ValidateModelState(actionCall);
            }

            return ExpressionParser.GetMethodName(actionCall);
        }

        private void PrepareController()
        {
            this.controller.Request = this.HttpRequestMessage;
            this.controller.Request.TransformToAbsoluteRequestUri();
            this.controller.RequestContext = this.HttpRequestMessage.GetRequestContext();
            this.controller.Configuration = this.HttpConfiguration ?? MyWebApi.Configuration ?? new HttpConfiguration();
            this.controller.User = MockedIPrincipal.CreateUnauthenticated();
        }

        private void ValidateModelState(LambdaExpression actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall);
            foreach (var argument in arguments)
            {
                this.Controller.Validate(argument.Value);
            }
        }
    }
}
