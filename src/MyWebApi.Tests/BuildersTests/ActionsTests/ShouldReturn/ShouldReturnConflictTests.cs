namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class ShouldReturnConflictTests
    {
        [Test]
        public void ShouldReturnConflictShouldNotThrowExceptionWhenActionReturnsConflict()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ConflictAction())
                .ShouldReturn()
                .Conflict();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be ConflictResult, but instead received BadRequestResult.")]
        public void ShouldReturnConflictShouldThrowExceptionWhenActionDoesNotReturnConflict()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Conflict();
        }
    }
}
