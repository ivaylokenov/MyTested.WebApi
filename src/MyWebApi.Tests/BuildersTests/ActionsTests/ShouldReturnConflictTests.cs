namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnConflictTests
    {
        [Test]
        public void ShouldReturnConflictShouldNotThrowExceptionWhenActionReturnsConflict()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ConflictAction())
                .ShouldReturnConflict();
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
                .ShouldReturnConflict();
        }
    }
}
