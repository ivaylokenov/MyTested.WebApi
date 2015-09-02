namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnEmptyTests
    {
        [Test]
        public void Should()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction());
        }
    }
}
