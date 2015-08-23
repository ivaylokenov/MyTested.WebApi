namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using System.Net;

    using Exceptions;

    using NUnit.Framework;

    using Setups;

    [TestFixture]
    public class ShouldReturnStatusCodeTests
    {
        [Test]
        public void ShouldReturnStatusCodeShouldNotThrowExceptionWhenActionReturnsStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturnStatusCode();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be StatusCodeResult, but instead received BadRequestResult.")]
        public void ShouldReturnStatusCodeShouldThrowExceptionWhenActionDoesNotReturnStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnStatusCode();
        }

        [Test]
        public void ShouldReturnStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturnStatusCode(HttpStatusCode.Found);
        }

        [Test]
        [ExpectedException(
            typeof(HttpStatusCodeAssertionException),
            ExpectedMessage = "When calling StatusCodeAction action in WebApiController expected to have 201 (Created) status code, but received 302 (Redirect).")]
        public void ShouldReturnStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturnStatusCode(HttpStatusCode.Created);
        }
    }
}
