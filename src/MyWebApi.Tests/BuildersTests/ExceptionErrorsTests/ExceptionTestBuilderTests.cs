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
    public class ExceptionTestBuilderTests
    {
        [Test]
        public void OfTypeShouldNotThrowExceptionWhenExceptionIsOfTheProvidedType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected InvalidOperationException, but instead received NullReferenceException.")]
        public void OfTypeShouldThrowExceptionWhenExceptionIsNotOfTheProvidedType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<InvalidOperationException>();
        }

        [Test]
        public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithCorrectMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .WithMessage("Test exception message")
                .AndAlso()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling InternalServerErrorWithExceptionAction action in WebApiController expected exception with message 'Exception message', but instead received 'Test exception message'.")]
        public void WithMessageShouldNotThrowExceptionWhenExceptionIsWithIncorrectMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.InternalServerErrorWithExceptionAction())
                .ShouldReturn()
                .InternalServerError()
                .WithException()
                .OfType<NullReferenceException>()
                .AndAlso()
                .WithMessage("Exception message");
        }
    }
}
