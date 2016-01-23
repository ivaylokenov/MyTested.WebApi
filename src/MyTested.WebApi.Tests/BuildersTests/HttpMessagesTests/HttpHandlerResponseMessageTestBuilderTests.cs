// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpMessagesTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Handlers;
    using Setups.Models;

    [TestFixture]
    public class HttpHandlerResponseMessageTestBuilderTests
    {
        [Test]
        public void WithResponseModelOfTypeShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<IEnumerable<ResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When testing ResponseMessageHandler expected HTTP response message model to be a ResponseModel, but instead received a different model.")]
        public void WithResponseModelOfTypeShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When testing ResponseMessageHandler expected HTTP response message model to be a ResponseModel, but instead received a different model.")]
        public void WithResponseModelOfTypeShouldThrowExceptionWithIncorrectHttpContent()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            var handler = new ResponseMessageHandler();

            MyWebApi
                .Handler(handler)
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModel(handler.ResponseModel);
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithDeeplyEqualResponseModel()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModel(TestObjectFactory.GetListOfResponseModels());
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When testing ResponseMessageHandler expected HTTP response message model to be the given model, but in fact it was a different model.")]
        public void WithResponseModelShouldThrowExceptionWithDeeplyUnequalResponseModel()
        {
            var another = TestObjectFactory.GetListOfResponseModels();
            another.Add(new ResponseModel());

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithResponseModel(another);
        }

        [Test]
        public void WithContentOfTypeShouldNotThrowExceptionWithCorrectContent()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithContentOfType<ObjectContent>();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content to be StreamContent, but was in fact ObjectContent. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithContentOfTypeShouldThrowExceptionWithIncorrectContent()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithContentOfType<StreamContent>();
        }

        [Test]
        public void WithStringContentOfTypeShouldNotThrowExceptionWithCorrectContent()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStringContent("Test string");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result string content to be 'Another string', but was in fact 'Test string'. Actual HTTP response message details: 
Status code: 200,
Headers: 
None,
Content: 
Test string.")]
        public void WithStringContentOfTypeShouldThrowExceptionWithIncorrectContent()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStringContent("Another string");
        }

        [Test]
        public void WithStringContentAndActionShouldNotThrowExceptionWithCorrectAssertions()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStringContent(content =>
                {
                    Assert.AreEqual("Test string", content);
                });
        }

        [Test]
        public void WithStringContentAndPredicateShouldNotThrowExceptionWithCorrectAssertions()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStringContent(content => content == "Test string");
        }

        [Test]
        [ExpectedException(
            typeof(HttpResponseMessageAssertionException),
            ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result Content to pass the given predicate, but but it failed. Actual HTTP response message details: 
Status code: 200,
Headers: 
None,
Content: 
Test string.")]
        public void WithStringContentAndPredicateShouldThrowExceptionWithIncorrectAssertions()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("StringContent", "StringContent");

            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStringContent(content => content == "Test");
        }

        [Test]
        public void WithMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithMediaTypeFormatter(new JsonMediaTypeFormatter());
        }

        [Test]
        public void WithMediaTypeFormatterOfTypeShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithMediaTypeFormatterOfType<CustomMediaTypeFormatter>();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithMediaTypeFormatterOfTypeShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        public void WithDefaultMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request => request.WithHeader("FromRequest", "FromRequest"))
                .ShouldReturnHttpResponseMessage()
                .WithDefaultMediaTypeFormatter();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithDefaultMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithDefaultMediaTypeFormatter();
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderName()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result headers to contain AnotherHeader, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingHeaderShouldThrowExceptionWithIncorrectHeaderName()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("AnotherHeader");
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValue()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader", "TestHeaderValue");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result headers to contain TestHeader with AnotherHeaderValue value, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValue()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader", "AnotherHeaderValue");
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "TestHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result headers to have TestHeader with AnotherHeaderValue value, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "AnotherHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result headers to contain TestHeader with 2 values, but instead found 1. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndOneCorrectAndOneIncorrectValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "TestHeaderValue", "AnotherHeaderValue" });
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectDictionaryOfHeaders()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result headers to be 2, but were in fact 1. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectDictionaryOfHeadersWithInvalidCount()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } },
                    { "AnotherTestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        public void ContainingContentHeaderShouldNotThrowExceptionWithCorrectHeaderName()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content headers to contain AnotherHeader, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingContentHeaderShouldThrowExceptionWithIncorrectHeaderName()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("AnotherHeader");
        }

        [Test]
        public void ContainingContentHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValue()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader", "TestHeaderValue");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content headers to contain TestHeader with AnotherHeaderValue value, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingContentHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValue()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader", "AnotherHeaderValue");
        }

        [Test]
        public void ContainingContentHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader", new[] { "TestHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content headers to have TestHeader with AnotherHeaderValue value, but none was found. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingContentHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader", new[] { "AnotherHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content headers to contain TestHeader with 2 values, but instead found 1. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingContentHeaderShouldThrowExceptionWithCorrectHeaderNameAndOneCorrectAndOneIncorrectValues()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("TestHeader", new[] { "TestHeaderValue", "AnotherHeaderValue" });
        }

        [Test]
        public void ContainingContentHeadersShouldNotThrowExceptionWithCorrectDictionaryOfHeaders()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content headers to be 2, but were in fact 1. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void ContainingContentHeadersShouldNotThrowExceptionWithCorrectDictionaryOfHeadersWithInvalidCount()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } },
                    { "AnotherTestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        [ExpectedException(
            typeof(HttpResponseMessageAssertionException),
            ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result content to be initialized and set, but it was null and no content headers were found. Actual HTTP response message details: 
Status code: 204,
Headers: 
None,
Content: 
Non-string value.")]
        public void ContainingHeaderShouldThrowExceptionIfNoContentIsAdded()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request => request.WithHeader("NoContent", "NoContent"))
                .ShouldReturnHttpResponseMessage()
                .ContainingContentHeader("AnotherHeader");
        }

        [Test]
        public void WithStatusCodeShouldNotThrowExceptionWithValidStatusCode()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result status code to be 400 (BadRequest), but instead received 200 (OK). Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithStatusCodeShouldThrowExceptionWithInvalidStatusCode()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersionAsString()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithVersion("1.1");
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersionWithMajorAndMinor()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithVersion(1, 1);
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersion()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithVersion(new Version(1, 1));
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result version to be 1.0, but instead received 1.1. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithVersionShouldThrowExceptionWithInvalidVersion()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithVersion("1.0");
        }

        [Test]
        public void WithReasonPhraseShouldNotThrowExceptionWithValidPhrase()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithReasonPhrase("Custom reason phrase");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result reason phrase to be 'Invalid reason phrase', but instead received 'Custom reason phrase'. Actual HTTP response message details: 
Status code: 200,
Headers: 
TestHeader - 'TestHeaderValue',
Content: 
Non-string value.")]
        public void WithReasonPhraseShouldThrowExceptionWithInvalidPhrase()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithReasonPhrase("Invalid reason phrase");
        }

        [Test]
        public void WithSuccessStatusCodeShouldNotThrowExceptionWithValidStatusCode()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = @"When testing ResponseMessageHandler expected HTTP response message result status code to be between 200 and 299, but it was not. Actual HTTP response message details: 
Status code: 404,
Headers: 
None,
Content: 
Non-string value.")]
        public void WithSuccessStatusCodeShouldThrowExceptionWithInvalidStatusCode()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(request => request.WithHeader("NotFound", "NotFound"))
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode();
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode()
                .AndAlso()
                .WithReasonPhrase("Custom reason phrase");
        }

        [Test]
        public void AndProvideTheHttpResponseMessageShouldWorkCorrectly()
        {
            var response = MyWebApi
                .Handler<ResponseMessageHandler>()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode()
                .AndAlso()
                .WithReasonPhrase("Custom reason phrase")
                .AndProvideTheHttpResponseMessage();

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
