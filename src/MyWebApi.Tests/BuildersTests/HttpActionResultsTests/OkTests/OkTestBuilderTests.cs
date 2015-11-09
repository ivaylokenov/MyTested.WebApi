// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpActionResultsTests.OkTests
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;

    [TestFixture]
    public class OkTestBuilderTests
    {
        [Test]
        public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithDefaultContentNegotiator();
        }

        [Test]
        [ExpectedException(
            typeof(OkResultAssertionException),
            ExpectedMessage = "When calling OkResultWithContentNegotiatorAction action in WebApiController expected ok result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .WithDefaultContentNegotiator();
        }

        [Test]
        public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .WithContentNegotiator(new CustomContentNegotiator());
        }

        [Test]
        public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .WithContentNegotiatorOfType<CustomContentNegotiator>();
        }

        [Test]
        public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter());
        }

        [Test]
        public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .ContainingDefaultFormatters();
        }

        [Test]
        [ExpectedException(typeof(
            OkResultAssertionException),
            ExpectedMessage = "When calling OkResultWithContentNegotiatorAction action in WebApiController expected ok result Formatters to be 4, but instead found 5.")]
        public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultHasNotDefaultMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .ContainingDefaultFormatters();
        }

        [Test]
        public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        }

        [Test]
        public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
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
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
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
                .Calling(c => c.OkResultWithContentNegotiatorAction())
                .ShouldReturn()
                .Ok()
                .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>()
                .AndAlso()
                .WithResponseModelOfType<int>();
        }
    }
}
