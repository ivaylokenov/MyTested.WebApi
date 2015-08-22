namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;

    using Common;
    using ControllerSetups;

    using NUnit.Framework;

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
        [ExpectedException(typeof(ArgumentException))]
        public void GetMethodNameShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            ExpressionParser.GetMethodName(expression);
        }

        [Test]
        public void ResolveMethodArgumentsShouldReturnCorrectCollectionOfTypeValuePair()
        {
            var requestModel = new RequestModel
            {
                Id = 2,
                Name = "Test"
            };

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
            Assert.AreEqual(2, secondArgumentAsRequestModel.Id);
            Assert.AreEqual("Test", secondArgumentAsRequestModel.Name);
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
        [ExpectedException(typeof(ArgumentException))]
        public void ResolveMethodArgumentsShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            ExpressionParser.ResolveMethodArguments(expression);
        }
    }
}
