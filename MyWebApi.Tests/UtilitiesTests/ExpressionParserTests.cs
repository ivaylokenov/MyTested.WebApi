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
            Expression<Func<NormalController, IHttpActionResult>> expression = c => c.EmptyAction();
            var methodName = ExpressionParser.GetMethodName(expression);
            Assert.AreEqual("EmptyAction", methodName);
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
