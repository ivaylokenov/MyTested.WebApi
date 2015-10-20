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

        public IAndResolvedRouteTestBuilder То<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

        public IAndResolvedRouteTestBuilder То<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController
        {
            return this.ResolveTo<TController>(actionCall);
        }

        public void ToNotAllowedMethod()
        {
            if (this.GetActualRouteInfo().MethodIsNotAllowed)
            {
                
            }
        }

        public void ToNonExistingRoute()
        {
            if (this.GetActualRouteInfo().IsResolved
                || this.GetActualRouteInfo().IsIgnored)
            {
                
            }
        }

        public void ToIgnoredRoute()
        {
            if (!this.GetActualRouteInfo().IsIgnored)
            {
                
            }
        }

        public IAndResolvedRouteTestBuilder ToHandlerOfType<THandler>()
            where THandler : HttpMessageHandler
        {
            return this;
        }

        public IAndResolvedRouteTestBuilder ToNoHandlerOfType<THandler>()
            where THandler : HttpMessageHandler
        {
            return this;
        }

        public IAndResolvedRouteTestBuilder ToNoHandler()
        {
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
                this.ThrowNewRouteAssertionException(actualRouteValues.UnresolvedError);
            }

            if (actualRouteValues.IsIgnored)
            {
                this.ThrowNewRouteAssertionException("it is ignored with StopRoutingHandler");
            }

            if (Reflection.AreDifferentTypes(expectedRouteInfo.Controller, actualRouteInfo.Controller))
            {
                this.ThrowNewRouteAssertionException(string.Format(
                    "instead matched {0}",
                    actualRouteValues.Controller.GetName()));
            }

            if (expectedRouteValues.Action != actualRouteValues.Action)
            {
                this.ThrowNewRouteAssertionException(string.Format(
                    "instead matched {0} action",
                    actualRouteValues.Action));
            }

            expectedRouteValues.Arguments.ForEach(arg =>
            {
                var expectedArgumentInfo = arg.Value;
                var actualArgumentInfo = actualRouteValues.ActionArguments[arg.Key];
                if (Reflection.AreNotDeeplyEqual(expectedArgumentInfo.Value, actualArgumentInfo.Value))
                {
                    this.ThrowNewRouteAssertionException(string.Format(
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

        private void ThrowNewRouteAssertionException(string message)
        {
            throw new RouteAssertionException(string.Format(
                    "Expected route '{0}' to match {1} action in {2} but {3}.",
                    this.requestMessage.RequestUri,
                    this.expectedRouteInfo.Action,
                    this.expectedRouteInfo.Controller.GetName(),
                    message));
        }
    }
}
