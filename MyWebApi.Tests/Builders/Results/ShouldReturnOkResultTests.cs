namespace MyWebApi.Tests.Builders.Results
{
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
                .ShouldReturnOkResult();
        }

        [Test]
        [ExpectedException(typeof(IHttpActionResultAssertionException))]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnOkResult();
        }
    }
}
