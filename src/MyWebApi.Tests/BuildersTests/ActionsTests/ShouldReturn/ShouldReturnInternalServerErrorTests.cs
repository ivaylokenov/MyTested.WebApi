namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
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
                .ShouldReturn()
                .InternalServerError();
        }

        [Test]
        public void ShouldReturnInternalServerErrorShouldNotThrowExceptionWhenResultIsExceptionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError();
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
                .ShouldReturn()
                .InternalServerError();
        }
    }
}
