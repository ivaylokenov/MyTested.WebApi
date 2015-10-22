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

    public partial class ShouldMapTestBuilder : BaseRouteTestBuilder, IAndShouldMapTestBuilder, IAndResolvedRouteTestBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        private LambdaExpression actionCallExpression;
        private ResolvedRouteInfo actualRouteInfo;
        private ExpressionParsedRouteInfo expectedRouteInfo;

        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            string url)
            : base(httpConfiguration)
        {
            this.requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        }

        public ShouldMapTestBuilder(
            HttpConfiguration httpConfiguration,
            HttpRequestMessage requestMessage)
            : base(httpConfiguration)
        {
            this.requestMessage = requestMessage;
        }

        public IAndResolvedRouteTestBuilder To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

        public IAndResolvedRouteTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

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

        public void ToNonExistingRoute()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (actualInfo.IsResolved
                || actualInfo.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(
                    "be non-existing",
                    string.Format(
                        "in fact it was {0}",
                        actualInfo.IsIgnored ? "ignored with StopRoutingHandler" : "resolved successfully"));
            }
        }

        public void ToIgnoredRoute()
        {
            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(
                    "be ignored with StopRoutingHandler",
                    string.Format(
                        "in fact {0}",
                        actualInfo.IsResolved ? "it was resolved successfully" : actualInfo.UnresolvedError));
            }
        }

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

        public IAndResolvedRouteTestBuilder ToValidModelState()
        {
            const string expectedErrorMessage = "have valid model state with no errors";

            var actualInfo = this.GetActualRouteInfo();
            if (!actualInfo.IsResolved || actualInfo.IsIgnored)
            {
                this.ThrowNewRouteAssertionException(
                    expectedErrorMessage,
                    actualInfo.IsIgnored ? "it was ignored with StopRoutingHandler" : actualInfo.UnresolvedError);
            }

            if (!actualInfo.ModelState.IsValid)
            {
                this.ThrowNewRouteAssertionException(
                    expectedErrorMessage,
                    "it had some");
            }

            return this;
        }

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

        public IShouldMapTestBuilder And()
        {
            return this;
        }

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

            if (Reflection.AreDifferentTypes(expectedRouteInfo.Controller, actualRouteInfo.Controller))
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
