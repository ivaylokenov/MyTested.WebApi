namespace MyWebApi.Tests.BuildersTests.JsonTests
{
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class JsonTestBuilderTests
    {
        [Test]
        public void WithResponseModelOfTypeShouldWorkCorrectlyWithJson()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithDefaultEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithEncodingAction action in WebApiController expected JSON result encoding to be System.Text.UTF8Encoding, but instead received System.Text.ASCIIEncoding.")]
        public void WithDefaultEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }
    }
}
