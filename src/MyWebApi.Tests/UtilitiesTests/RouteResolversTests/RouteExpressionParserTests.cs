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

namespace MyWebApi.Tests.UtilitiesTests.RouteResolversTests
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;
    using Utilities.RouteResolvers;

    [TestFixture]
    public class RouteExpressionParserTests
    {
        [Test]
        public void ParseShouldParseVoidMethods()
        {
            Expression<Action<WebApiController>> expression = c => c.EmptyAction();
            var result = RouteExpressionParser.Parse<WebApiController>(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WebApiController), result.Controller);
            Assert.AreEqual("EmptyAction", result.Action);
            Assert.AreEqual(0, result.Arguments.Count);
        }

        [Test]
        public void ParseShouldParseReturningMethods()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultAction();
            var result = RouteExpressionParser.Parse<WebApiController>(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WebApiController), result.Controller);
            Assert.AreEqual("OkResultAction", result.Action);
            Assert.AreEqual(0, result.Arguments.Count);
        }

        [Test]
        public void ParseShouldParseVoidMethodsWithParameters()
        {
            var requestModel = new RequestModel();
            Expression<Action<WebApiController>> expression = c => c.EmptyActionWithParameters(1, requestModel);
            var result = RouteExpressionParser.Parse<WebApiController>(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WebApiController), result.Controller);
            Assert.AreEqual("EmptyActionWithParameters", result.Action);
            Assert.AreEqual(2, result.Arguments.Count);
            Assert.AreEqual(1, result.Arguments["id"].Value);
            Assert.AreEqual(requestModel, result.Arguments["model"].Value);
        }

        [Test]
        public void ParseShouldParseReturningMethodsWithParameters()
        {
            var requestModel = new RequestModel();
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultActionWithRequestBody(1, requestModel);
            var result = RouteExpressionParser.Parse<WebApiController>(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WebApiController), result.Controller);
            Assert.AreEqual("OkResultActionWithRequestBody", result.Action);
            Assert.AreEqual(2, result.Arguments.Count);
            Assert.AreEqual(1, result.Arguments["id"].Value);
            Assert.AreEqual(requestModel, result.Arguments["model"].Value);
        }

        [Test]
        public void ParseShouldParseMethodWithActionNameAttribute()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.VariousAttributesAction();
            var result = RouteExpressionParser.Parse<WebApiController>(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(WebApiController), result.Controller);
            Assert.AreEqual("NormalAction", result.Action);
            Assert.AreEqual(0, result.Arguments.Count);
        }
    }
}
