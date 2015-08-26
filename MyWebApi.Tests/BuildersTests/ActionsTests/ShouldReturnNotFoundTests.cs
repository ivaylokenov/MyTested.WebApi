namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;

    using NUnit.Framework;

    using Setups;

    [TestFixture]
    public class ShouldReturnNotFoundTests
    {
        [Test]
        public void ShouldReturnNotFoundShouldNotThrowExceptionWhenActionReturnsNotFound()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NotFoundAction())
                .ShouldReturnNotFound();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be NotFoundResult, but instead received BadRequestResult.")]
        public void ShouldReturnNotFoundShouldThrowExceptionWhenActionDoesNotReturnNotFound()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnNotFound();
        }
    }
}
