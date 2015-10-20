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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Common.Extensions;
    using Common.Routes;
    using Contracts.Routes;
    using Exceptions;
    using Utilities;
    using Utilities.RouteResolvers;

    public class ShouldMapTestBuilder : BaseRouteTestBuilder, IShouldMapTestBuilder, IAndResolvedRouteTestBuilder
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

        public IShouldMapTestBuilder WithHttpMethod(string httpMethod)
        {
            return this.WithHttpMethod(new HttpMethod(httpMethod));
        }

        public IShouldMapTestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            this.requestMessage.Method = httpMethod;
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithRequestHeader(h.Key, h.Value));
            return this;
        }

        public IShouldMapTestBuilder WithRequestHeaders(HttpRequestHeaders headers)
        {
            return this.WithRequestHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IShouldMapTestBuilder WithContentHeader(string name, string value)
        {
            this.requestMessage.Content.Headers.Add(name, value);
            return this;
        }

        public IShouldMapTestBuilder WithContentHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Content.Headers.Add(name, values);
            return this;
        }

        public IShouldMapTestBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithContentHeader(h.Key, h.Value));
            return this;
        }

        public IShouldMapTestBuilder WithContentHeaders(HttpContentHeaders headers)
        {
            return this.WithContentHeaders(headers.ToDictionary(h => h.Key, h => h.Value));
        }

        public IShouldMapTestBuilder WithFormUrlEncodedContent(string content)
        {
            this.SetRequestContent(content, MediaType.FormUrlEncoded);
            return this;
        }

        public IShouldMapTestBuilder WithJsonContent(string content)
        {
            this.SetRequestContent(content, MediaType.ApplicationJson);
            return this;
        }

        public IShouldMapTestBuilder WithContent(string content, string mediaType)
        {
            this.SetRequestContent(content, mediaType);
            return this;
        }

        public IShouldMapTestBuilder WithContent(string content, MediaTypeHeaderValue mediaType)
        {
            this.SetRequestContent(content, mediaType.MediaType);
            return this;
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
                    string.Format("don't allow method '{0}'", this.requestMessage.Method.Method),
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
                        actualInfo.IsResolved ? "resolved successfully" : "ignored with StopRoutingHandler"));
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
                        "in fact it was {0}",
                        actualInfo.IsResolved ? "in fact it was resolved successfully" : actualInfo.UnresolvedError));
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
                    string.Format("in fact found {0}", actualHandlerType == null ? null : actualHandlerType.ToFriendlyTypeName()));
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
            return this;
        }

        public IAndResolvedRouteTestBuilder ToInvalidModelState(int? withNumberOfErrors = null)
        {
            return this;
        }

        public IAndResolvedRouteTestBuilder ToModelStateFor<TRequestModel>()
        {
            return this;
        }

        public IResolvedRouteTestBuilder AndAlso()
        {
            return this;
        }

        private void SetRequestContent(string content, string mediaType)
        {
            this.requestMessage.Content = new StringContent(content);
            this.requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
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
                this.ThrowNewRouteAssertionExceptionWithExpectedMatch(actualRouteValues.UnresolvedError);
            }

            if (actualRouteValues.IsIgnored)
            {
                this.ThrowNewRouteAssertionExceptionWithExpectedMatch("it is ignored with StopRoutingHandler");
            }

            if (Reflection.AreDifferentTypes(expectedRouteInfo.Controller, actualRouteInfo.Controller))
            {
                this.ThrowNewRouteAssertionExceptionWithExpectedMatch(string.Format(
                    "instead matched {0}",
                    actualRouteValues.Controller.GetName()));
            }

            if (expectedRouteValues.Action != actualRouteValues.Action)
            {
                this.ThrowNewRouteAssertionExceptionWithExpectedMatch(string.Format(
                    "instead matched {0} action",
                    actualRouteValues.Action));
            }

            expectedRouteValues.Arguments.ForEach(arg =>
            {
                var expectedArgumentInfo = arg.Value;
                var actualArgumentInfo = actualRouteValues.ActionArguments[arg.Key];
                if (Reflection.AreNotDeeplyEqual(expectedArgumentInfo.Value, actualArgumentInfo.Value))
                {
                    this.ThrowNewRouteAssertionExceptionWithExpectedMatch(string.Format(
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

        private void ThrowNewRouteAssertionExceptionWithExpectedMatch(string message)
        {
            this.ThrowNewRouteAssertionException(
                string.Format(
                    "match {0} action in {1}",
                    this.expectedRouteInfo.Action,
                    this.expectedRouteInfo.Controller.ToFriendlyTypeName()),
                message);
        }

        private void ThrowNewRouteAssertionException(string expected, string actual)
        {
            throw new RouteAssertionException(string.Format(
                    "Expected route '{0}' to {1} but {2}.",
                    this.requestMessage.RequestUri,
                    expected,
                    actual));
        }
    }
}
