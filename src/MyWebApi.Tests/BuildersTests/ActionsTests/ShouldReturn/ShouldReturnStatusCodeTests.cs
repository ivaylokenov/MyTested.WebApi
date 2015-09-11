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

namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using System.Net;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnStatusCodeTests
    {
        [Test]
        public void ShouldReturnStatusCodeShouldNotThrowExceptionWhenActionReturnsStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be StatusCodeResult, but instead received BadRequestResult.")]
        public void ShouldReturnStatusCodeShouldThrowExceptionWhenActionDoesNotReturnStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .StatusCode();
        }

        [Test]
        public void ShouldReturnStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode(HttpStatusCode.Found);
        }

        [Test]
        [ExpectedException(
            typeof(HttpStatusCodeResultAssertionException),
            ExpectedMessage = "When calling StatusCodeAction action in WebApiController expected to have 201 (Created) status code, but received 302 (Redirect).")]
        public void ShouldReturnStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode(HttpStatusCode.Created);
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be StatusCodeResult, but instead received BadRequestResult.")]
        public void ShouldReturnStatusCodeShouldThrowExceptionWhenActionDoesNotReturnStatusCodeAndPassingStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .StatusCode(HttpStatusCode.Created);
        }
    }
}
