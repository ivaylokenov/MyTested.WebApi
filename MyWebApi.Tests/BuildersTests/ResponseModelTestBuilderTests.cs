namespace MyWebApi.Tests.BuildersTests
{
    using System.Collections.Generic;
    using System.Linq;

    using ControllerSetups;
    using Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class ResponseModelTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithIncorrectInheritedTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<IList<ResponseModel>>();
        }

        [Test]
        [ExpectedException(typeof(ResponseModelAssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<ResponseModel>();
        }

        [Test]
        [ExpectedException(typeof(ResponseModelAssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectGenericTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<ICollection<int>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller(() => controller)
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        [ExpectedException(typeof(ResponseModelAssertionException))]
        public void WithResponceModelShouldThrowExceptionWithDifferentPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<ICollection<ResponseModel>>(m =>
                {
                    Assert.AreEqual(2, m.Count);
                    Assert.AreEqual(1, m.First().Id);
                });
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOkResult()
                .WithResponseModel<ICollection<ResponseModel>>(m =>
                {
                    Assert.AreEqual(3, m.Count);
                    Assert.AreEqual(1, m.First().Id);
                });
        }
    }
}
