namespace MyWebApi.Tests.BuildersTests.ResultsTests
{
    using ControllerSetups;

    using Exceptions;

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
