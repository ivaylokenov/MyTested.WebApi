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
    public class ShouldReturnRedirectTests
    {
        [Test]
        public void ShouldReturnRedirectShouldNotThrowExceptionWithRedirectResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectAction())
                .ShouldReturn()
                .Redirect();
        }

        [Test]
        public void ShouldReturnRedirectShouldNotThrowExceptionWithRedirectToRouteResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be RedirectResult, but instead received BadRequestResult.")]
        public void ShouldReturnRedirectShouldThrowExceptionWithBadRequestResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Redirect();
        }
    }
}
