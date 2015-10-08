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
    using System.Linq;
    using System.Net.Http;
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
        public void AndProvideShouldReturnProperHttpRequestMessage()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request 
                    => request
                        .WithMethod(HttpMethod.Get)
                        .WithHeader("TestHeader", "TestHeaderValue"))
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .AndProvideTheHttpRequestMessage();

            Assert.IsNotNull(httpRequestMessage);
            Assert.AreEqual(HttpMethod.Get, httpRequestMessage.Method);
            Assert.IsTrue(httpRequestMessage.Headers.Contains("TestHeader"));
        }

        [Test]
        public void AndProvideTheControllerAttributesShouldReturnProperAttributes()
        {
            var attributes = MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes()
                .AndProvideTheControllerAttributes();

            Assert.AreEqual(2, attributes.Count());
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
        public void AndProvideShouldReturnProperActionAttributes()
        {
            var attributes = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes()
                .AndProvideTheActionAttributes();

            Assert.AreEqual(6, attributes.Count());
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

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "Void methods cannot provide action result because they do not have return value.")]
        public void AndProvideShouldThrowExceptionIfActionIsVoid()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldHave()
                .ValidModelState()
                .AndProvideTheActionResult();
        }
    }
}
