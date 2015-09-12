// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.BuildersTests.HttpActionResultsTests.InternalServerErrorTests
{
    using System;
    using Exceptions;
    using Setups.Controllers;
    using NUnit.Framework;

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
