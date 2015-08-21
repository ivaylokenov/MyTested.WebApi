namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;

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
    }
}
