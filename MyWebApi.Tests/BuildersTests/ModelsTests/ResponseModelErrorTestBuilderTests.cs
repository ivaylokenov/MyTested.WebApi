namespace MyWebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;

    using Exceptions;
    using Setups;
    using Setups.Models;

    using NUnit.Framework;

    [TestFixture]
    public class ResponseModelErrorTestBuilderTests
    {
        [Test]
        public void ContainingNoErrorsShouldNotThrowExceptionWhenThereAreNoModelStateErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody))
                .ShouldReturnOk()
                .WithResponseModelOfType<List<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelErrorAssertionException),
            ExpectedMessage = "When calling OkResultActionWithRequestBody action in WebApiController expected to have valid model state with no errors, but it had some.")]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModelOfType<List<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }

        [Test]
        public void AndModelStateErrorShouldNotThrowExceptionWhenTheProvidedModelStateErrorExists()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingModelStateError("RequiredString");
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have a model error against key Name, but none found.")]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExist()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturnOk()
                .WithResponseModel(requestBody)
                .ContainingModelStateError("Name");
        }

        [Test]
        public void AndModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have a model error against key RequiredString, but none found.")]
        public void AndModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturnOk()
                .WithResponseModel(requestBody)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturnOk()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have no model errors against key RequiredString, but found some.")]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenChainedWithValidModel()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturnOk()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.Integer)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have no model errors against key Integer, but found some.")]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenChainedWithInvalidModel()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingNoModelStateErrorFor(r => r.Integer)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }
    }
}
