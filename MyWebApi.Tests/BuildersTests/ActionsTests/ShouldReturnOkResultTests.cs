namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using ControllerSetups;

    using NUnit.Framework;

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
        [ExpectedException(typeof(HttpActionResultAssertionException))]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnOk();
        }
    }
}
