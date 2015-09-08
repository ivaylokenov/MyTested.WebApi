namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnNullOrDefaultTests
    {
        [Test]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueIsNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .Null();
        }
    }
}
