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

namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using System;
    using System.Net;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldThrowExceptionTests
    {
        [Test]
        public void ShouldThrowExceptionShouldCatchAndValidateThereIsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception();
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController thrown exception was expected, but in fact none was caught.")]
        public void ShouldThrowExceptionShouldThrowIfNoExceptionIsCaught()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldThrow()
                .Exception();
        }

        [Test]
        public void ShouldThrowExceptionShouldCatchAndValidateTypeOfException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithException action in WebApiController expected InvalidOperationException, but instead received NullReferenceException.")]
        public void ShouldThrowExceptionShouldThrowWithInvalidTypeOfException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<InvalidOperationException>();
        }

        [Test]
        public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithHttpResponseException())
                .ShouldThrow()
                .HttpResponseException();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithException action in WebApiController expected HttpResponseException, but instead received NullReferenceException.")]
        public void ShouldThrowHttpResponseExceptionShouldThrowIfTheExceptionIsNotValidType()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .HttpResponseException();
        }
    }
}
