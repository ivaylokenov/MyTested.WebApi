// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnCreatedTests
    {
        [Test]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created();
        }

        [Test]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtRouteResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be CreatedNegotiatedContentResult<T> or CreatedAtRouteNegotiatedContentResult<T>, but instead received BadRequestResult.")]
        public void ShouldReturnCreatedShouldThrowExceptionWithBadRequestResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Created();
        }
    }
}
