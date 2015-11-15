// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpActionResultsTests.InternalServerErrorTests
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class InternalServerErrorTestBuilderTests
    {
        [Test]
        public void WithExceptionShouldNotThrowExceptionWhenActionResultIsExceptionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException();
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorAction action in WebApiController expected internal server error result to contain exception, but it could not be found.")]
        public void WithExceptionShouldThrowExceptionWhenActionResultIsInternalServerErrorResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException();
        }

        [Test]
        public void WithExceptionShouldNotThrowExceptionWhenProvidedExceptionIsOfTheSameType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException(new NullReferenceException("Test exception message"));
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected internal server error result to contain InvalidOperationException, but instead received NullReferenceException.")]
        public void WithExceptionShouldThrowExceptionWhenProvidedExceptionIsNotOfTheSameType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException(new InvalidOperationException("Test exception message"));
        }

        [Test]
        [ExpectedException(
            typeof(InternalServerErrorResultAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected internal server error result to contain exception with message 'Exception message', but instead received 'Test exception message'.")]
        public void WithExceptionShouldThrowExceptionWhenProvidedExceptionIsOfTheSameTypeWithDifferentMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException(new NullReferenceException("Exception message"));
        }
    }
}
