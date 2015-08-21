namespace MyWebApi.Tests.BuildersTests
{
    using System.Collections.Generic;

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
    }
}
