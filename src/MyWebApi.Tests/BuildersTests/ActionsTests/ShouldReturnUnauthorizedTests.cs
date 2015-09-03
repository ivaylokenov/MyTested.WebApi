namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnUnauthorizedTests
    {
        [Test]
        public void ShouldReturnUnauthorizedShouldNotThrowExceptionWhenActionReturnsUnauthorizedResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedAction())
                .ShouldReturnUnauthorized();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be UnauthorizedResult, but instead received BadRequestResult.")]
        public void ShouldReturnUnauthorizedShouldThrowExceptionWhenActionDoesNotReturnUnauthorizedResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnUnauthorized();
        }
    }
}
