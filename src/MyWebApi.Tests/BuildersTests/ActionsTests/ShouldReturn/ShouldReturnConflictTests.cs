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
    public class ShouldReturnConflictTests
    {
        [Test]
        public void ShouldReturnConflictShouldNotThrowExceptionWhenActionReturnsConflict()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ConflictAction())
                .ShouldReturn()
                .Conflict();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be ConflictResult, but instead received BadRequestResult.")]
        public void ShouldReturnConflictShouldThrowExceptionWhenActionDoesNotReturnConflict()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Conflict();
        }
    }
}
