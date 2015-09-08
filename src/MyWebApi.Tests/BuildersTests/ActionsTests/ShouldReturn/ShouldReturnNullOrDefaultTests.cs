namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
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

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "Boolean cannot be null.")]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNullable()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .Null();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action result to be null, but instead received IHttpActionResult.")]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Null();
        }

        [Test]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIDefaultForClass()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Test]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIsNotDefaultForStructs()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.DefaultStructAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action result to be the default value of IHttpActionResult, but in fact it was not.")]
        public void ShouldReturnDefaultShouldThrowExceptionWhenReturnValueIsNotDefault()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .DefaultValue();
        }
    }
}
