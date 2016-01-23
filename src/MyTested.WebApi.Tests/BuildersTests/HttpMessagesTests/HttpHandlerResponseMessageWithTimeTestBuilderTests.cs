// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpMessagesTests
{
    using System.Net;
    using System.Net.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class HttpHandlerResponseMessageWithTimeTestBuilderTests
    {
        [Test]
        public void WithResponseTimePredicateShouldWorkCorrectly()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithResponseTime(
                    responseTime => responseTime.TotalMilliseconds > 0 && responseTime.TotalMilliseconds < int.MaxValue);
        }

        [Test]
        public void WithResponseTimeAssertionsShouldWorkCorrectly()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithResponseTime(responseTime =>
                {
                    Assert.IsTrue(responseTime.TotalMilliseconds > 0);
                    Assert.IsTrue(responseTime.TotalMilliseconds < int.MaxValue);
                });
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithResponseTime(responseTime => responseTime.TotalMilliseconds > 0)
                .AndAlso()
                .WithStatusCode(HttpStatusCode.Found);
        }

        [Test]
        public void AndProvideTheResponseTimeShouldWorkCorrectly()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            var responseTime = MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .AndProvideTheResponseTime();

            Assert.IsTrue(responseTime.TotalMilliseconds > 0);
        }

        [Test]
        [ExpectedException(
            typeof(HttpResponseMessageAssertionException),
            ExpectedMessage = @"When testing ServerHttpMessageHandler expected HTTP response message result response time to pass the given condition, but it failed. Actual HTTP response message details: 
Status code: 302,
Headers: 
None,
Content: 
Found!.")]
        public void WithResponseTimeShouldThrowExceptionIfPredicateIsNotValid()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithResponseTime(
                    responseTime => responseTime.TotalMilliseconds < 0);
        }
    }
}
