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
    public class ShouldReturnHttpResponseModelTests
    {
        [Test]
        public void ShouldReturnHttpResponseMessageShouldNotThrowExceptionIfResultIsHttpResponseMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be HttpResponseMessage, but instead received BadRequestResult.")]
        public void ShouldReturnHttpResponseMessageShouldThrowExceptionIfResultIsNotHttpResponseMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .HttpResponseMessage();
        }
    }
}
