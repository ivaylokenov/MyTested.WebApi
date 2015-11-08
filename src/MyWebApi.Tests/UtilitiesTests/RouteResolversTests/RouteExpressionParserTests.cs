// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.UtilitiesTests.RouteResolversTests
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
