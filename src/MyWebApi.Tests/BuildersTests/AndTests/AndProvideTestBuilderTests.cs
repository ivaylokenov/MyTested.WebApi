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

namespace MyWebApi.Tests.BuildersTests.AndTests
{
    using System;
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class AndProvideTestBuilderTests
    {
        [Test]
        public void AndProvideShouldReturnProperController()
        {
            var controller = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .AndProvideTheController();

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
        }

        [Test]
        public void AndProvideShouldReturnProperActionName()
        {
            var actionName = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .AndProvideTheActionName();

            Assert.AreEqual("BadRequestWithErrorAction", actionName);
        }

        [Test]
        public void AndProvideShouldReturnProperActionResult()
        {
            var actionResult = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode()
                .AndProvideTheActionResult();

            Assert.IsNotNull(actionResult);
            Assert.IsAssignableFrom<StatusCodeResult>(actionResult);
        }

        [Test]
        public void AndProvideShouldReturnProperCaughtException()
        {
            var caughtException = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .AndProvideTheCaughtException();

            Assert.IsNotNull(caughtException);
            Assert.IsAssignableFrom<NullReferenceException>(caughtException);
        }
    }
}
