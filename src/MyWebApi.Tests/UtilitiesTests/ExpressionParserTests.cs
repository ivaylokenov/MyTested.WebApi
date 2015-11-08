// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;
    using Common;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Utilities;

    [TestFixture]
    public class ExpressionParserTests
    {
        [Test]
        public void GetMethodNameShouldReturnCorrectMethodNameWithValidMethodCallExpression()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultAction();
            var methodName = ExpressionParser.GetMethodName(expression);
            Assert.AreEqual("OkResultAction", methodName);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Provided expression is not a valid method call.")]
        public void GetMethodNameShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            ExpressionParser.GetMethodName(expression);
        }

        [Test]
        public void ResolveMethodArgumentsShouldReturnCorrectCollectionOfArgumentsInformation()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultActionWithRequestBody(1, requestModel);

            var result = ExpressionParser.ResolveMethodArguments(expression);

            var arguments = result as IList<MethodArgumentInfo> ?? result.ToArray();
            var firstArgument = arguments.First();
            var secondArgument = arguments.Last();
            var secondArgumentAsRequestModel = secondArgument.Value as RequestModel;

            Assert.AreEqual("id", firstArgument.Name);
            Assert.AreEqual(typeof(int), firstArgument.Type);
            Assert.AreEqual(1, firstArgument.Value);

            Assert.AreEqual("model", secondArgument.Name);
            Assert.AreEqual(typeof(RequestModel), secondArgument.Type);
            Assert.IsNotNull(secondArgumentAsRequestModel);
            Assert.AreEqual(2, secondArgumentAsRequestModel.Integer);
            Assert.AreEqual("Test", secondArgumentAsRequestModel.RequiredString);
        }

        [Test]
        public void ResolveMethodArgumentsShouldReturnEmptyCollectionIfMethodDoesNotHaveArguments()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultAction();
            var result = ExpressionParser.ResolveMethodArguments(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ResolveMethodArgumentsShouldReturnProperArgumentsWithNullValues()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultActionWithRequestBody(1, null);
            var result = ExpressionParser.ResolveMethodArguments(expression).ToList();

            var first = result[0];
            var second = result[1];

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, first.Value);
            Assert.IsNull(second.Value);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Provided expression is not a valid method call.")]
        public void ResolveMethodArgumentsShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            ExpressionParser.ResolveMethodArguments(expression);
        }

        [Test]
        public void GetPropertyNameShouldReturnProperMemberNameWithValidExpression()
        {
            Expression<Func<WebApiController, object>> expression = c => c.InjectedService;
            var result = ExpressionParser.GetPropertyName(expression);

            Assert.AreEqual("InjectedService", result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Provided expression is not a valid member expression.")]
        public void GetPropertyNameShouldThrowExceptionWithInvalidMemberExpression()
        {
            Expression<Func<WebApiController, object>> expression = c => c.OkResultWithResponse();
            ExpressionParser.GetPropertyName(expression);
        }

        [Test]
        public void GetMethodAttributesShouldReturnProperAttributes()
        {
            Expression<Func<WebApiController, object>> expression = c => c.VariousAttributesAction();
            var attributes = ExpressionParser.GetMethodAttributes(expression).Select(a => a.GetType()).ToList();

            var expectedTypes = new List<Type>
            {
                typeof(AllowAnonymousAttribute),
                typeof(RouteAttribute),
                typeof(ActionNameAttribute),
                typeof(NonActionAttribute),
                typeof(AcceptVerbsAttribute),
                typeof(HttpDeleteAttribute)
            };

            Assert.IsNotNull(attributes);
            Assert.AreEqual(6, attributes.Count());

            var allAttributesArePresent = expectedTypes.All(attributes.Contains);
            Assert.IsTrue(allAttributesArePresent);
        }
    }
}
