namespace MyWebApi.Tests.BuildersTests.ResponseModelsTests
{
    using System.Collections.Generic;

    using ControllerSetups;
    using ControllerSetups.Models;
    using Exceptions;

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
                .WithResponseModel<List<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel<List<ResponseModel>>()
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
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
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
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
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
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
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
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
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
