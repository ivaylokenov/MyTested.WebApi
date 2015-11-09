// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpMessagesTests
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Handlers;

    [TestFixture]
    public class HttpMessageHandlerTestBuilderTests
    {
        [Test]
        public void WithInnerHandlerWithoutConstructionFunctionShouldPopulateCorrectInnerHandler()
        {
            var handler = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler<CustomMessageHandler>()
                .AndProvideTheHandler() as DelegatingHandler;

            Assert.IsNotNull(handler);
            Assert.IsNotNull(handler.InnerHandler);
            Assert.IsAssignableFrom<CustomMessageHandler>(handler.InnerHandler);
        }

        [Test]
        public void WithInnerHandlerWithProvidedInstanceShouldPopulateCorrectInnerHandler()
        {
            var innerHandler = new CustomMessageHandler();
            var handler = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler(innerHandler)
                .AndProvideTheHandler() as DelegatingHandler;

            Assert.IsNotNull(handler);
            Assert.IsNotNull(handler.InnerHandler);
            Assert.AreSame(innerHandler, handler.InnerHandler);
        }

        [Test]
        [ExpectedException(
            typeof(HttpHandlerAssertionException),
            ExpectedMessage = "When adding inner handler CustomMessageHandler to CustomMessageHandler, expected CustomMessageHandler to be DelegatingHandler, but in fact was not.")]
        public void WithInnerHandlerInstanceShouldThrowExceptionIfOuterHandlerIsNotDelegatingHandler()
        {
            var innerHandler = new CustomMessageHandler();
            var handler = MyWebApi
                .Handler<CustomMessageHandler>()
                .WithInnerHandler(innerHandler)
                .AndProvideTheHandler() as DelegatingHandler;
        }

        [Test]
        public void WithInnerHandlerWithConstructionFunctionShouldPopulateCorrectInnerHandler()
        {
            var handler = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler(() => new CustomMessageHandler())
                .AndProvideTheHandler() as DelegatingHandler;

            Assert.IsNotNull(handler);
            Assert.IsNotNull(handler.InnerHandler);
            Assert.IsAssignableFrom<CustomMessageHandler>(handler.InnerHandler);
        }

        [Test]
        public void WithInnerHandlerBuilderShouldPopulateCorrectInnerHandler()
        {
            var outerHandler = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler<CustomDelegatingHandler>(
                    firstInnerHandler => firstInnerHandler.WithInnerHandler<CustomDelegatingHandler>(
                        secondInnerHandler => secondInnerHandler.WithInnerHandler(() => new CustomMessageHandler())))
                .AndProvideTheHandler() as DelegatingHandler;

            Assert.IsNotNull(outerHandler);
            Assert.IsAssignableFrom<CustomDelegatingHandler>(outerHandler);

            var firstHandler = outerHandler.InnerHandler as DelegatingHandler;
            Assert.IsNotNull(firstHandler);
            Assert.IsAssignableFrom<CustomDelegatingHandler>(firstHandler);

            var secondHandler = firstHandler.InnerHandler as DelegatingHandler;
            Assert.IsNotNull(secondHandler);
            Assert.IsAssignableFrom<CustomDelegatingHandler>(secondHandler);

            var thirdHandler = secondHandler.InnerHandler;
            Assert.IsNotNull(thirdHandler);
            Assert.IsAssignableFrom<CustomMessageHandler>(thirdHandler);
        }

        [Test]
        public void WithRequestMessageShouldPopulateCorrectHttpRequestMessage()
        {
            var originalRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://test.com"));

            var request = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithHttpRequestMessage(originalRequestMessage)
                .AndProvideTheHttpRequestMessage();

            Assert.AreSame(originalRequestMessage, request);
        }

        [Test]
        public void WithRequestMessageBuilderShouldPopulateCorrectHttpRequestMessage()
        {
            var actualRequest = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler<CustomDelegatingHandler>(firstHandler => firstHandler.WithInnerHandler<CustomMessageHandler>())
                .WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(HttpMethod.Get, actualRequest.Method);
        }

        [Test]
        public void ShouldReturnHttpResponseMessageShouldWorkCorrectly()
        {
            MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithInnerHandler<CustomMessageHandler>()
                .WithHttpRequestMessage(request => request.WithHeader("CustomHeader", "CustomValue"))
                .ShouldReturnHttpResponseMessage();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "InvalidOperationException with 'Handler error' message was thrown but was not caught or expected.")]
        public void ShouldReturnHttpResponseMessageShouldThrowExceptionWhenHandlerThrowsException()
        {
            MyWebApi
                .Handler<ExceptionMessageHandler>()
                .WithHttpRequestMessage(request => request.WithHeader("CustomHeader", "CustomValue"))
                .ShouldReturnHttpResponseMessage();
        }

        [Test]
        public void WithoutAnyConfigurationShouldInstantiateDefaultOne()
        {
            MyWebApi.IsUsing(null);

            var config = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .AndProvideTheHttpConfiguration();

            Assert.IsNotNull(config);

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }

        [Test]
        public void WithHttpConfigurationShouldOverrideTheDefaultOne()
        {
            var config = new HttpConfiguration();

            var controllerConfig = MyWebApi
                .Handler<CustomDelegatingHandler>()
                .WithHttpConfiguration(config)
                .AndProvideTheHttpConfiguration();

            Assert.AreSame(config, controllerConfig);
        }
    }
}
