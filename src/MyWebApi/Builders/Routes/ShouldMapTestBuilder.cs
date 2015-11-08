// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Routes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Web.Http;
    using Common.Extensions;
    using Common.Routes;
    using Contracts.Routes;
    using Exceptions;
    using Utilities;
    using Utilities.RouteResolvers;

    /// <summary>
    /// Used for building and testing a route.
    /// </summary>
    public partial class ShouldMapTestBuilder : BaseRouteTestBuilder, IAndShouldMapTestBuilder, IAndResolvedRouteTestBuilder
    {
        private const string ExpectedModelStateErrorMessage = "have valid model state with no errors";

        private readonly HttpRequestMessage requestMessage;

        private LambdaExpression actionCallExpression;
        private ResolvedRouteInfo actualRouteInfo;
        private ExpressionParsedRouteInfo expectedRouteInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the route test.</param>
        /// <param name="location">URI location represented by string.</param>
        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            string location)
            : base(httpConfiguration)
        {
            this.requestMessage = new HttpRequestMessage(HttpMethod.Get, location);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldMapTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the route test.</param>
        /// <param name="requestMessage">HTTP request message to use for the route test.</param>
        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            HttpRequestMessage requestMessage)
            : base(httpConfiguration)
        {
            this.requestMessage = requestMessage;
        }

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether the built route is resolved to the action provided by the expression.
        /// </summary>
        /// <typeparam name="TController">Type of expected resolved controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected resolved action.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

        /// <summary>
        /// Tests whether the built route cannot be resolved because of not allowed method.
        /// </summary>
        public void ToNotAllowedMethod()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.MethodIsNotAllowed)
            {
                this.ThrowNewRouteAssertionException(
                    string.Format("not allow method '{0}'", this.requestMessage.Method.Method),
                    actualInfo.IsResolved ? "in fact it was allowed" : actualInfo.UnresolvedError);
            }
        }

        /// <summary>
        /// Tests whether the built route cannot be resolved.
        /// </summary>
        public void ToNonExistingRoute()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (actualInfo.IsResolved
                || actualInfo.IsIgnored)
            {
                var actualErrorMessage = string.Format(
                    "in fact it was {0}",
                    actualInfo.IsIgnored ? "ignored with StopRoutingHandler" : "resolved successfully");

                this.ThrowNewRouteAssertionException(
                    "be non-existing",
                    actualErrorMessage);
            }
        }

        /// <summary>
        /// Tests whether the built route is ignored by StopRoutingHandler.
        /// </summary>
        public void ToIgnoredRoute()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsIgnored)
            {
                var actualErrorMessage = string.Format(
                    "in fact {0}",
                    actualInfo.IsResolved ? "it was resolved successfully" : actualInfo.UnresolvedError);

                this.ThrowNewRouteAssertionException(
                    "be ignored with StopRoutingHandler",
                    actualErrorMessage);
            }
        }

        /// <summary>
        /// Tests whether the resolved route will be handled by a HttpMessageHandler of the provided type.
        /// </summary>
        /// <typeparam name="THandler">Type of HttpMessageHandler.</typeparam>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToHandlerOfType<THandler>()
            where THandler : HttpMessageHandler
        {
            var expectedHandlerType = typeof(THandler);
            var actualHandlerType = this.GetActualRouteInfo().HttpMessageHandler == null
                ? null
                : this.GetActualRouteInfo().HttpMessageHandler.GetType();

            if (Reflection.AreDifferentTypes(expectedHandlerType, actualHandlerType))
            {
                this.ThrowNewRouteAssertionException(
                    string.Format("be handled by {0}", expectedHandlerType.ToFriendlyTypeName()),
                    string.Format("in fact found {0}", actualHandlerType == null ? "no handler at all" : actualHandlerType.ToFriendlyTypeName()));
            }

            return this;
        }

        /// <summary>
        /// Tests whether the resolved route will not be handled by a HttpMessageHandler of the provided type.
        /// </summary>
        /// <typeparam name="THandler">Type of HttpMessageHandler.</typeparam>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToNoHandlerOfType<THandler>()
            where THandler : HttpMessageHandler
        {
            var actualHandler = this.GetActualRouteInfo().HttpMessageHandler;
            if (actualHandler != null)
            {
                var expectedHandlerType = typeof(THandler);
                var actualHandlerType = actualHandler.GetType();

                if (Reflection.AreSameTypes(expectedHandlerType, actualHandlerType))
                {
                    this.ThrowNewRouteAssertionException(
                        string.Format("not be handled by {0}", expectedHandlerType.ToFriendlyTypeName()),
                        "in fact found the same type of handler");
                }
            }
            
            return this;
        }

        /// <summary>
        /// Tests whether the resolved route will not be handled by any HttpMessageHandler.
        /// </summary>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToNoHandler()
        {
            var actualHandler = this.GetActualRouteInfo().HttpMessageHandler;
            if (actualHandler != null)
            {
                this.ThrowNewRouteAssertionException(
                    "have no handler of any type",
                    string.Format("in fact found {0}", actualHandler.GetName()));
            }

            return this;
        }

        /// <summary>
        /// Tests whether the resolved route will have valid model state.
        /// </summary>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToValidModelState()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsResolved || actualInfo.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(
                    ExpectedModelStateErrorMessage,
                    actualInfo.IsIgnored ? "it was ignored with StopRoutingHandler" : actualInfo.UnresolvedError);
            }

            if (!actualInfo.ModelState.IsValid)
            {
                this.ThrowNewRouteAssertionException(
                    ExpectedModelStateErrorMessage,
                    "it had some");
            }

            return this;
        }

        /// <summary>
        /// Tests whether the resolved route will have invalid model state.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same route test builder.</returns>
        public IAndResolvedRouteTestBuilder ToInvalidModelState(int? withNumberOfErrors = null)
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsResolved || actualInfo.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(
                    "have invalid model state",
                    actualInfo.IsIgnored ? "it was ignored with StopRoutingHandler" : actualInfo.UnresolvedError);
            }

            var actualModelStateErrors = actualInfo.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                this.ThrowNewRouteAssertionException(
                    string.Format(
                        "have invalid model state{0}",
                        withNumberOfErrors == null ? string.Empty : string.Format(" with {0} errors", withNumberOfErrors)),
                    withNumberOfErrors == null ? "was in fact valid" : string.Format("in fact contained {0}", actualModelStateErrors));
            }

            return this;
        }

        /// <summary>
        /// And method for better readability when building route HTTP request message.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IShouldMapTestBuilder And()
        {
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when building route tests.
        /// </summary>
        /// <returns>The same route builder.</returns>
        public IResolvedRouteTestBuilder AndAlso()
        {
            return this;
        }

        private IAndResolvedRouteTestBuilder ResolveTo<TController>(LambdaExpression actionCall)
            where TController : ApiController
        {
            this.actionCallExpression = actionCall;
            this.ValidateRouteInformation<TController>();
            return this;
        }

        private void ValidateRouteInformation<TController>()
            where TController : ApiController
        {
            var expectedRouteValues = this.GetExpectedRouteInfo<TController>();
            var actualRouteValues = this.GetActualRouteInfo();

            if (!actualRouteValues.IsResolved)
            {
                this.ThrowNewRouteAssertionException(actual: actualRouteValues.UnresolvedError);
            }

            if (actualRouteValues.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(actual: "it was ignored with StopRoutingHandler");
            }

            if (Reflection.AreDifferentTypes(this.expectedRouteInfo.Controller, this.actualRouteInfo.Controller))
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0}",
                    actualRouteValues.Controller.ToFriendlyTypeName()));
            }

            if (expectedRouteValues.Action != actualRouteValues.Action)
            {
                this.ThrowNewRouteAssertionException(actual: string.Format(
                    "instead matched {0} action",
                    actualRouteValues.Action));
            }

            expectedRouteValues.Arguments.ForEach(arg =>
            {
                var expectedArgumentInfo = arg.Value;
                var actualArgumentInfo = actualRouteValues.ActionArguments[arg.Key];
                if (Reflection.AreNotDeeplyEqual(expectedArgumentInfo.Value, actualArgumentInfo.Value))
                {
                    this.ThrowNewRouteAssertionException(actual: string.Format(
                        "the '{0}' parameter was different",
                        arg.Key));
                }
            });
        }

        private ExpressionParsedRouteInfo GetExpectedRouteInfo<TController>()
            where TController : ApiController
        {
            return this.expectedRouteInfo ??
                   (this.expectedRouteInfo = RouteExpressionParser.Parse<TController>(this.actionCallExpression));
        }

        private ResolvedRouteInfo GetActualRouteInfo()
        {
            return this.actualRouteInfo ??
                   (this.actualRouteInfo = InternalRouteResolver.Resolve(this.HttpConfiguration, this.requestMessage));
        }

        private void ThrowNewRouteAssertionException(string expected = null, string actual = null)
        {
            if (string.IsNullOrWhiteSpace(expected))
            {
                expected = string.Format(
                    "match {0} action in {1}",
                    this.expectedRouteInfo.Action,
                    this.expectedRouteInfo.Controller.ToFriendlyTypeName());
            }

            throw new RouteAssertionException(string.Format(
                    "Expected route '{0}' to {1} but {2}.",
                    this.requestMessage.RequestUri,
                    expected,
                    actual));
        }
    }
}
