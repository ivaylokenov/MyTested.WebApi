// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.HttpActionResultsTests.BadRequestTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class BadRequestErrorMessageTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .ThatEquals("Bad request");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to be 'Bad', but instead found 'Bad request'.")]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .ThatEquals("Bad");
        }

        [Test]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .BeginningWith("Bad ");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to begin with 'request', but instead found 'Bad request'.")]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .BeginningWith("request");
        }

        [Test]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .EndingWith("request");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to end with 'Bad', but instead found 'Bad request'.")]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .EndingWith("Bad");
        }

        [Test]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .Containing("d r");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request error message to contain 'Another', but instead found 'Bad request'.")]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .Containing("Another");
        }
    }
}
