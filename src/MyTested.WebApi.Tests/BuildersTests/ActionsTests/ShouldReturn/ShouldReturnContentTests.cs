// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnContentTests
    {
        [Test]
        public void ShouldReturnContentShouldNotThrowExceptionWithNegotiatedContentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content();
        }

        [Test]
        public void ShouldReturnContentShouldNotThrowExceptionWithMediaTypeContentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be NegotiatedContentResult<T> or FormattedContentResult<T>, but instead received BadRequestResult.")]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Content();
        }
    }
}
