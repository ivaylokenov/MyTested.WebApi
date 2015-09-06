namespace MyWebApi.Tests.BuildersTests.JsonTests
{
    using System.Collections.Generic;
    using System.Text;
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
            ExpectedMessage = "When calling JsonWithEncodingAction action in WebApiController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        public void WithDefaultEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }

        [Test]
        public void WithEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithEncoding(Encoding.ASCII);
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithEncodingAction action in WebApiController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        public void WithEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithEncoding(Encoding.UTF8);
        }

        [Test]
        public void WithDefaultJsonSettingsShouldNotThrowExeptionWithDefaultJsonSettings()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding()
                .AndAlso()
                .WithDefaulJsonSerializerSettings();
        }
    }
}
