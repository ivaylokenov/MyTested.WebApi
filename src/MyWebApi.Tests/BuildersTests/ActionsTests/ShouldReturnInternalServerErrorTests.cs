namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnInternalServerErrorTests
    {
        [Test]
        public void ShouldReturnInternalServerErrorShouldNotThrowExceptionWhenResultIsInternalServerErrorResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorAction())
                .ShouldReturnInternalServerError();
        }

        [Test]
        public void ShouldReturnInternalServerErrorShouldNotThrowExceptionWhenResultIsExceptionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturnInternalServerError();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be InternalServerErrorResult, but instead received BadRequestResult.")]
        public void ShouldReturnNotFoundShouldThrowExceptionWhenActionDoesNotReturnNotFound()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnInternalServerError();
        }
    }
}
