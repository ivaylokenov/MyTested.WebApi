// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.HttpActionResultsTests.ContentTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ContentTestBuilderTests
    {
        [Test]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Test]
        [ExpectedException(
            typeof(ContentResultAssertionException),
            ExpectedMessage = "When calling ContentAction action in WebApiController expected to have 404 (NotFound) status code, but received 200 (OK).")]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(TestObjectFactory.MediaType);
        }

        [Test]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
        }

        [Test]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(MediaType.ApplicationJson);
        }

        [Test]
        [ExpectedException(
            typeof(ContentResultAssertionException),
            ExpectedMessage = "When calling ContentActionWithMediaType action in WebApiController expected content result MediaType to be text/plain, but instead received application/json.")]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(new MediaTypeHeaderValue("text/plain"));
        }

        [Test]
        [ExpectedException(
            typeof(ContentResultAssertionException),
            ExpectedMessage = "When calling ContentActionWithMediaType action in WebApiController expected content result MediaType to be null, but instead received application/json.")]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType((MediaTypeHeaderValue)null);
        }

        [Test]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithNullMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType((MediaTypeHeaderValue)null);
        }

        [Test]
        [ExpectedException(
            typeof(ContentResultAssertionException),
            ExpectedMessage = "When calling ContentActionWithNullMediaType action in WebApiController expected content result MediaType to be application/json, but instead received null.")]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithNullMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
        }

        [Test]
        public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithDefaultContentNegotiator();
        }

        [Test]
        [ExpectedException(
            typeof(ContentResultAssertionException),
            ExpectedMessage = "When calling ContentActionWithCustomFormatters action in WebApiController expected content result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .WithDefaultContentNegotiator();
        }

        [Test]
        public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .WithContentNegotiator(new CustomContentNegotiator());
        }

        [Test]
        public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .WithContentNegotiatorOfType<CustomContentNegotiator>();
        }

        [Test]
        public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .ContainingMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        [Test]
        public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .ContainingDefaultFormatters();
        }

        [Test]
        [ExpectedException(typeof(
            ContentResultAssertionException),
            ExpectedMessage = "When calling ContentActionWithMediaType action in WebApiController expected content result Formatters to be 4, but instead found 1.")]
        public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultHasNotDefaultMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .ContainingDefaultFormatters();
        }

        [Test]
        public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        }

        [Test]
        public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .ContainingMediaTypeFormatters(
                    formatters => formatters
                        .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter())
                        .AndAlso()
                        .ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>());
        }

        [Test]
        public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormattersAsParams()
        {
            var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentActionWithCustomFormatters())
                .ShouldReturn()
                .Content()
                .ContainingMediaTypeFormatters(
                    mediaTypeFormatters[0],
                    mediaTypeFormatters[1],
                    mediaTypeFormatters[2],
                    mediaTypeFormatters[3],
                    mediaTypeFormatters[4]);
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }
    }
}
