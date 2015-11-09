// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Handlers;
    using Setups.Models;

    [TestFixture]
    public class HttpHandlerModelDetailsTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(2, m.Count);
                    Assert.AreEqual(1, m.First().IntegerValue);
                });
        }

        [Test]
        [ExpectedException(
            typeof(AssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(1, m.First().IntegerValue);
                    Assert.AreEqual(3, m.Count);
                });
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 1);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When testing ResponseMessageHandler expected HTTP response message content model IList<ResponseModel> to pass the given condition, but it failed.")]
        public void WithResponseModelShouldThrowExceptionWithWrongPredicate()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<IList<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 2);
        }

        [Test]
        public void AndProvideTheModelShouldWorkCorrectly()
        {
            var model = MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<IList<ResponseModel>>()
                .AndProvideTheModel();

            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
        }
    }
}
