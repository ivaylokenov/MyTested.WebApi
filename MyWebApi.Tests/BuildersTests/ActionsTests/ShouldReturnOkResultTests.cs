namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class ShouldReturnOkResultTests
    {
        [Test]
        public void ShouldReturnOkResultShouldNotThrowExceptionWithOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturnOk();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "")]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnOk();
        }
    }
}
