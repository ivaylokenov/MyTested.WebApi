// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Handlers;
    using Setups.Services;

    [TestFixture]
    public class MyWebApiTests
    {
        [Test]
        public void IsUsingShouldOverrideTheDefaultConfiguration()
        {
            // run two test cases in order to check the configuration is global
            var configs = new List<HttpConfiguration>();
            for (int i = 0; i < 2; i++)
            {
                var controller = MyWebApi.Controller<WebApiController>().AndProvideTheController();
                var actualConfig = controller.Configuration;

                Assert.IsNotNull(actualConfig);
                configs.Add(actualConfig);
            }

            Assert.AreSame(configs[0], configs[1]);
        }

        [Test]
        public void ControllerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyWebApi.Controller<WebApiController>().AndProvideTheController();
            
            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
        }

        [Test]
        public void ControllerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyWebApi.Controller(() => new WebApiController(new InjectedService())).AndProvideTheController();

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
            
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsAssignableFrom<InjectedService>(controller.InjectedService);
        }

        [Test]
        public void ControllerWithProvidedInstanceShouldPopulateCorrectInstanceOfControllerType()
        {
            var instance = new WebApiController();
            var controller = MyWebApi.Controller(instance).AndProvideTheController();

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
        }

        [Test]
        [ExpectedException(
            typeof(UnresolvedDependenciesException),
            ExpectedMessage = "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.")]
        public void ControllerWithNoParameterlessConstructorShouldThrowProperException()
        {
            MyWebApi
                .Controller<NoParameterlessConstructorController>()
                .Calling(c => c.OkAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        public void HandlerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        {
            var handler = MyWebApi
                .Handler<CustomMessageHandler>()
                .AndProvideTheHandler();

            Assert.IsNotNull(handler);
            Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        }

        [Test]
        public void HandlerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfHandlerType()
        {
            var handler = MyWebApi.Handler(() => new CustomMessageHandler()).AndProvideTheHandler();

            Assert.IsNotNull(handler);
            Assert.IsAssignableFrom<CustomMessageHandler>(handler);
        }

        [Test]
        public void HandlerWithProvidedInstanceShouldPopulateCorrectInstanceOfHandlerType()
        {
            var instance = new CustomMessageHandler();
            var controller = MyWebApi.Handler(instance).AndProvideTheHandler();

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<CustomMessageHandler>(controller);
        }

        [Test]
        public void IsRegisteredWithShouldWorkCorrectly()
        {
            MyWebApi.IsRegisteredWith(WebApiConfig.Register);

            Assert.IsNotNull(MyWebApi.Configuration);
            Assert.AreEqual(1, MyWebApi.Configuration.Routes.Count);

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }

        [Test]
        public void IsUsingDefaultConfigurationShouldWorkCorrectly()
        {
            MyWebApi.IsUsingDefaultHttpConfiguration();

            Assert.IsNotNull(MyWebApi.Configuration);
            Assert.IsTrue(MyWebApi.Configuration.Routes.ContainsKey("API Default"));

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }
    }
}
