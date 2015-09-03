namespace MyWebApi.Tests.BuildersTests.BadRequestsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class BadRequestErrorMessageTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .ThatEquals("Bad request");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to be 'Bad', but instead found 'Bad request'.")]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .ThatEquals("Bad");
        }

        [Test]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .BeginningWith("Bad ");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to begin with 'request', but instead found 'Bad request'.")]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .BeginningWith("request");
        }

        [Test]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .EndingWith("request");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to end with 'Bad', but instead found 'Bad request'.")]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .EndingWith("Bad");
        }

        [Test]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .Containing("d r");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to contain 'Another', but instead found 'Bad request'.")]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .Containing("Another");
        }
    }
}
