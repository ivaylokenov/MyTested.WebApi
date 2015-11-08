// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.ExceptionErrorsTests
{
    using System.Net;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class HttpResponseExceptionTestBuilderTests
    {
        [Test]
        public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseExceptionStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        [ExpectedException(
            typeof(HttpStatusCodeResultAssertionException),
            ExpectedMessage = "When calling ActionWithHttpResponseException action in WebApiController expected HttpResponseException to have 202 (Accepted) status code, but received 404 (NotFound).")]
        public void ShouldThrowHttpResponseExceptionShouldThrowWithInvalidHttpResponseExceptionStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException()
                .WithStatusCode(HttpStatusCode.Accepted);
        }

        [Test]
        public void ShouldThrowHttpResponseExceptionShouldBeAbleToTestHttpResponseMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseExceptionAndHttpResponseMessageException())
                .ShouldThrow()
                .HttpResponseException()
                .WithHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
