namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnBadRequestTests
    {
        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequest()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsInvalidModelStateResult()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest();
        }

        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequestErrorMessageResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling NotFoundAction action in WebApiController expected action result to be BadRequestResult, but instead received NotFoundResult.")]
        public void ShouldReturnNotFoundShouldThrowExceptionWhenActionDoesNotReturnNotFound()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NotFoundAction())
                .ShouldReturn()
                .BadRequest();
        }
    }
}
