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
        public void ResolveMethodArgumentsShouldReturnCorrectCollectionOfTypeValuePair()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultActionWithRequestBody(1, requestModel);

            var result = ExpressionParser.ResolveMethodArguments(expression);

            var typeValuePairs = result as IList<TypeValuePair> ?? result.ToArray();
            var firstArgument = typeValuePairs.First();
            var secondArgument = typeValuePairs.Last();
            var secondArgumentAsRequestModel = secondArgument.Value as RequestModel;

            Assert.AreEqual(typeof(int), firstArgument.Type);
            Assert.AreEqual(1, firstArgument.Value);

            Assert.AreEqual(typeof(RequestModel), secondArgument.Type);
            Assert.IsNotNull(secondArgumentAsRequestModel);
            Assert.AreEqual(2, secondArgumentAsRequestModel.Integer);
            Assert.AreEqual("Test", secondArgumentAsRequestModel.RequiredString);
        }

        [Test]
        public void ResolveMethodArgumentsShouldReturnEmptyCollectionIfMethoDoesNotHaveArguments()
        {
            Expression<Func<WebApiController, IHttpActionResult>> expression = c => c.OkResultAction();
            var result = ExpressionParser.ResolveMethodArguments(expression);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
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
