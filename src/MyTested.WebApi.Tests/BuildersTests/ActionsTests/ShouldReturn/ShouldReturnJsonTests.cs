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
    public class ShouldReturnJsonTests
    {
        [Test]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsJson()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be JsonResult<T>, but instead received BadRequestResult.")]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsNotJson()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Json();
        }
    }
}
