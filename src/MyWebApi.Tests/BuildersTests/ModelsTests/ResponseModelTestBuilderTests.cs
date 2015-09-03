namespace MyWebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ResponseModelTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithIncorrectInheritedTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<IList<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectImplementatorTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<List<ResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected response model of type ResponseModel, but instead received null.")]
        public void WithResponseModelShouldThrowExceptionWithNoResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturnOk()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithInterfaceResponse action in WebApiController expected response model to be a ResponseModel, but instead received a ICollection<ResponseModel>.")]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithInterfaceResponse action in WebApiController expected response model to be a ICollection<Int32>, but instead received a ICollection<ResponseModel>.")]
        public void WithResponseModelShouldThrowExceptionWithIncorrectGenericTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ICollection<int>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller(() => controller)
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturnOk()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected response model ICollection<ResponseModel> to be the given model, but in fact it was a different model.")]
        public void WithResponceModelShouldThrowExceptionWithDifferentPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        public void WithNoResponseModelShouldNotThrowExceptionWhenNoResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturnOk()
                .WithNoResponseModel();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected to not have response model but in fact response model was found.")]
        public void WithNoResponseModelShouldThrowExceptionWhenResponseModelExists()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithNoResponseModel();
        }
    }
}
