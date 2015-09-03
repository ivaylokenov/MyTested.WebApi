namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using System;
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
    }
}
