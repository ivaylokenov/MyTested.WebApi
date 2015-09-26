﻿// MyWebApi - ASP.NET Web API Fluent Testing Framework
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

namespace MyWebApi.Tests.BuildersTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class HttpResponseMessageTestBuilderTests
    {
        [Test]
        public void WithResponseModelOfTypeShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithResponseModelOfType<IEnumerable<ResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message model to be a ResponseModel, but instead received a List<ResponseModel>.")]
        public void WithResponseModelOfTypeShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller(controller)
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        [ExpectedException(
             typeof(ResponseModelAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message model to be the given model, but in fact it was a different model.")]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithResponseModel(TestObjectFactory.GetListOfResponseModels());
        }

        [Test]
        public void WithContentOfTypeShouldNotThrowExceptionWithCorrectContent()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithContentOfType<ObjectContent>();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result content to be StreamContent, but was in fact ObjectContent.")]
        public void WithContentOfTypeShouldThrowExceptionWithIncorrectContent()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithContentOfType<StreamContent>();
        }

        [Test]
        public void WithMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithMediaTypeFormatter())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageWithMediaTypeFormatter action in WebApiController expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found.")]
        public void WithMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithMediaTypeFormatter())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithMediaTypeFormatter(new JsonMediaTypeFormatter());
        }

        [Test]
        public void WithMediaTypeFormatterOfTypeShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithMediaTypeFormatter())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithMediaTypeFormatterOfType<CustomMediaTypeFormatter>();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageWithMediaTypeFormatter action in WebApiController expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found.")]
        public void WithMediaTypeFormatterOfTypeShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithMediaTypeFormatter())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        public void WithDefaultMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithResponseModelAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithDefaultMediaTypeFormatter();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageWithMediaTypeFormatter action in WebApiController expected HTTP response message result Formatters to contain JsonMediaTypeFormatter, but none was found.")]
        public void WithDefaultMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithMediaTypeFormatter())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithDefaultMediaTypeFormatter();
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderName()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result headers to contain AnotherHeader, but but none was found.")]
        public void ContainingHeaderShouldThrowExceptionWithIncorrectHeaderName()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("AnotherHeader");
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader", "TestHeaderValue");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result headers to contain TestHeader with AnotherHeaderValue value, but none was found.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader", "AnotherHeaderValue");
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaderNameAndValues()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "TestHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result headers to have TestHeader with AnotherHeaderValue value, but none was found.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndIncorrectValues()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "AnotherHeaderValue" });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result headers to contain TestHeader with 2 values, but instead found 1.")]
        public void ContainingHeaderShouldThrowExceptionWithCorrectHeaderNameAndOneCorrectAndOneIncorrectValues()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeader("TestHeader", new[] { "TestHeaderValue", "AnotherHeaderValue" });
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectDictionaryOfHeaders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result headers to be 2, but were in fact 1.")]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectDictionaryOfHeadersWithInvalidCount()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { "TestHeader", new List<string> { "TestHeaderValue" } },
                    { "AnotherTestHeader", new List<string> { "TestHeaderValue" } }
                });
        }

        [Test]
        public void ContainingHeaderShouldNotThrowExceptionWithCorrectHeaders()
        {
            var httpReponseResponse = new HttpResponseMessage();
            httpReponseResponse.Headers.Add("TestHeader", "TestHeaderValue");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .ContainingHeaders(httpReponseResponse.Headers);
        }

        [Test]
        public void WithStatusCodeShouldNotThrowExceptionWithValidStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result status code to be 400 (BadRequest), but instead received 200 (OK).")]
        public void WithStatusCodeShouldThrowExceptionWithInvalidStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersionAsString()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithVersion("1.1");
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersionWithMajorAndMinor()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithVersion(1, 1);
        }

        [Test]
        public void WithVersionShouldNotThrowExceptionWithValidVersion()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithVersion(new Version(1, 1));
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result version to be 1.0, but instead received 1.1.")]
        public void WithVersionShouldThrowExceptionWithInvalidVersion()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithVersion("1.0");
        }

        [Test]
        public void WithReasonPhraseShouldNotThrowExceptionWithValidPhrase()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithReasonPhrase("Custom reason phrase");
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageAction action in WebApiController expected HTTP response message result reason phrase to be 'Invalid reason phrase', but instead received 'Custom reason phrase'.")]
        public void WithReasonPhraseShouldThrowExceptionWithInvalidPhrase()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithReasonPhrase("Invalid reason phrase");
        }

        [Test]
        public void WithSuccessStatusCodeShouldNotThrowExceptionWithValidStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithSuccessStatusCode();
        }

        [Test]
        [ExpectedException(
             typeof(HttpResponseMessageAssertionException),
             ExpectedMessage = "When calling HttpResponseMessageWithResponseModelAction action in WebApiController expected HTTP response message result status code to be between 200 and 299, but it was not.")]
        public void WithSuccessStatusCodeShouldThrowExceptionWithInvalidStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageWithResponseModelAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithSuccessStatusCode();
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.HttpResponseMessageAction())
                .ShouldReturn()
                .HttpResponseMessage()
                .WithSuccessStatusCode()
                .AndAlso()
                .WithReasonPhrase("Custom reason phrase");
        }
    }
}
