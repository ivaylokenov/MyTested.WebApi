// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.BuildersTests.AndTests
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups;
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
        public void AndProvideShouldReturnProperHttpConfiguration()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();

            var actualConfig = MyWebApi
                .Controller<WebApiController>()
                .WithHttpConfiguration(config)
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .AndProvideTheHttpConfiguration();

            Assert.AreSame(config, actualConfig);
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
