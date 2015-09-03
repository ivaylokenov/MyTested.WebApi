namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class ShouldReturnNotFoundTests
    {
        [Test]
        public void ShouldReturnNotFoundShouldNotThrowExceptionWhenActionReturnsNotFound()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NotFoundAction())
                .ShouldReturn()
                .NotFound();
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
                .ShouldReturn()
                .NotFound();
        }
    }
}
