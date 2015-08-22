namespace MyWebApi.Tests.BuildersTests
{
    using System.Collections.Generic;

    using ControllerSetups;
    using Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class ResponseModelErrorTestBuilderTests
    {
        [Test]
        public void ContainingNoErrorsShouldNotThrowExceptionWhenThereAreNoModelStateErrors()
        {
            var requestBody = new RequestModel
            {
                Id = 1,
                Name = "Test"
            };

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
            var requestBodyWithErrors = new RequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                .ShouldReturnOk()
                .WithResponseModel<List<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }
    }
}
