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
