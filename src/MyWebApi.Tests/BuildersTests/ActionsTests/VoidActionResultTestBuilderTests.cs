namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
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
    }
}
