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
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnCreatedTests
    {
        [Test]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created();
        }

        [Test]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtRouteResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be CreatedNegotiatedContentResult<T> or CreatedAtRouteNegotiatedContentResult<T>, but instead received BadRequestResult.")]
        public void ShouldReturnCreatedShouldThrowExceptionWithBadRequestResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .Created();
        }
    }
}
