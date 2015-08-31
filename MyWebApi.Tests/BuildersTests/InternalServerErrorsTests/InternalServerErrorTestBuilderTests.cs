namespace MyWebApi.Tests.BuildersTests.InternalServerErrorsTests
{
    using System;
    using Exceptions;
    using Setups;
    using NUnit.Framework;

    [TestFixture]
    public class InternalServerErrorTestBuilderTests
    {
        [Test]
        public void WithExceptionShouldNotThrowExceptionWhenActionResultIsExceptionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturnInternalServerError()
                .WithException();
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorAction action in WebApiController expected internal server error result to contain exception, but it could not be found.")]
        public void WithExceptionShouldThrowExceptionWhenActionResultIsInternalServerErrorResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorAction())
                .ShouldReturnInternalServerError()
                .WithException();
        }

        [Test]
        public void WithExceptionShouldNotThrowExceptionWhenProvidedExceptionIsOfTheSameType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturnInternalServerError()
                .WithException(new NullReferenceException("Test exception message"));
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected internal server error result to contain InvalidOperationException, but instead received NullReferenceException.")]
        public void WithExceptionShouldThrowExceptionWhenProvidedExceptionIsNotOfTheSameType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturnInternalServerError()
                .WithException(new InvalidOperationException("Test exception message"));
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected internal server error result to contain exception with message 'Exception message', but instead received 'Test exception message'.")]
        public void WithExceptionShouldThrowExceptionWhenProvidedExceptionIsOfTheSameTypeWithDifferentMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturnInternalServerError()
                .WithException(new NullReferenceException("Exception message"));
        }
    }
}
