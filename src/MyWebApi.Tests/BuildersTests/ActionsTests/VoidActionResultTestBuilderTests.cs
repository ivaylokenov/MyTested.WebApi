namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class VoidActionResultTestBuilderTests
    {
        [Test]
        public void ShouldReturnEmptyShouldNotThrowExceptionWithNormalVoidAction()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction())
                .ShouldReturnEmpty();
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.")]
        public void ShouldReturnEmptyShouldThrowExceptionIfActionThrowsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldReturnEmpty();
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "AggregateException with 'One or more errors occurred.' message was thrown but was not caught or expected.")]
        public void ShouldReturnEmptyWithAsyncShouldThrowExceptionIfActionThrowsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.EmptyActionWithExceptionAsync())
                .ShouldReturnEmpty();
        }
    }
}
