namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldThrowExceptionTests
    {
        [Test]
        public void ShouldThrowExceptionShouldCatchAndValidateThereIsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldReturnEmpty();
        }
    }
}
