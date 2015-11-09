// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnOkResultTests
    {
        [Test]
        public void ShouldReturnOkResultShouldNotThrowExceptionWithOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.")]
        public void ShouldReturnOkWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.ActionWithExceptionAsync())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be OkNegotiatedContentResult<T>, but instead received BadRequestResult.")]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Ok();
        }
    }
}
