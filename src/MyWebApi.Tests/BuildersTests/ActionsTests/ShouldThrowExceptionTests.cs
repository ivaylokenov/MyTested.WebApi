namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using System;
    using System.Net;
    using Exceptions;
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
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception();
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController thrown exception was expected, but in fact none was caught.")]
        public void ShouldThrowExceptionShouldThrowIfNoExceptionIsCaught()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldThrow()
                .Exception();
        }

        [Test]
        public void ShouldThrowExceptionShouldCatchAndValidateTypeOfException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithException action in WebApiController expected InvalidOperationException, but instead received NullReferenceException.")]
        public void ShouldThrowExceptionShouldThrowWithInvalidTypeOfException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<InvalidOperationException>();
        }

        [Test]
        public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithException action in WebApiController expected HttpResponseException, but instead received NullReferenceException.")]
        public void ShouldThrowHttpResponseExceptionShouldThrowIfTheExceptionIsNotValidType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .HttpResponseException();
        }

        [Test]
        public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseExceptionStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        [ExpectedException(
            typeof(HttpStatusCodeAssertionException),
            ExpectedMessage = "When calling ActionWithHttpResponseException action in WebApiController expected HttpResponseException to have 202 (Accepted) status code, but received 404 (NotFound).")]
        public void ShouldThrowHttpResponseExceptionShouldThrowWithInvalidHttpResponseExceptionStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException()
                .WithStatusCode(HttpStatusCode.Accepted);
        }
    }
}
