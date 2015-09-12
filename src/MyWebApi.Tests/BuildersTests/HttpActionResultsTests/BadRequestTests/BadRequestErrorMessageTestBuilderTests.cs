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

namespace MyWebApi.Tests.BuildersTests.HttpActionResultsTests.BadRequestTests
{
    using Exceptions;
    using Setups.Controllers;
    using NUnit.Framework;

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
