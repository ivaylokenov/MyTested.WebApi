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

namespace MyWebApi.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ExceptionMessageTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().ThatEquals("Test exception message");
        }

        [Test]
        public void ThatEqualsShouldNotThrowExceptionWithProperErrorMessageAndFirstCallingWithMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .WithMessage().ThatEquals("Test exception message")
                .AndAlso()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to be 'Test', but instead found 'Test exception message'.")]
        public void ThatEqualsShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().ThatEquals("Test");
        }

        [Test]
        public void BeginningWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().BeginningWith("Test ");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to begin with 'exception', but instead found 'Test exception message'.")]
        public void BeginningWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().BeginningWith("exception");
        }

        [Test]
        public void EndingWithShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().EndingWith("message");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to end with 'Test', but instead found 'Test exception message'.")]
        public void EndingWithShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().EndingWith("Test");
        }

        [Test]
        public void ContainingShouldNotThrowExceptionWithProperErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().Containing("n m");
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception message to contain 'Another', but instead found 'Test exception message'.")]
        public void ContainingShouldThrowExceptionWithIncorrectErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage().Containing("Another");
        }
    }
}
