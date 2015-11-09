// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class CreatedTestBuilderTests
    {
        [Test]
        public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithDefaultContentNegotiator();
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in WebApiController expected created result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .WithDefaultContentNegotiator();
        }

        [Test]
        public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .WithContentNegotiator(new CustomContentNegotiator());
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result IContentNegotiator to be CustomContentNegotiator, but instead received DefaultContentNegotiator.")]
        public void WithContentNegotiatorShouldThrowExceptionWhenActionReturnsIncorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithContentNegotiator(new CustomContentNegotiator());
        }

        [Test]
        public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .WithContentNegotiatorOfType<CustomContentNegotiator>();
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result IContentNegotiator to be CustomContentNegotiator, but instead received DefaultContentNegotiator.")]
        public void WithContentNegotiatorOfTypeShouldThrowExceptionWhenActionReturnsIncorrectContentNegotiator()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithContentNegotiatorOfType<CustomContentNegotiator>();
        }

        [Test]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost.com/someuri/1?query=Test");
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.")]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost.com/");
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result location to be URI valid, but instead received http://somehost!@#?Query==true.")]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost!@#?Query==true");
        }

        [Test]
        public void AtLocationWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.")]
        public void AtLocationWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(new Uri("http://somehost.com/"));
        }

        [Test]
        public void AtLocationWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(location =>
                    location
                        .WithHost("somehost.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result URI to equal the provided one, but was in fact different.")]
        public void AtLocationWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(location =>
                    location
                        .WithHost("somehost12.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }

        [Test]
        public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter());
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        public void ContainingMediaTypeFormatterShouldThrowExceptionWhenActionResultDoesNotHaveTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        [Test]
        public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAction action in WebApiController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        public void ContainingMediaTypeFormatterOfTypeShouldThrowExceptionWhenActionResultDoesNotHaveTheProvidedMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>();
        }

        [Test]
        public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ContainingDefaultFormatters();
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in WebApiController expected created result Formatters to be 4, but instead found 5.")]
        public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultDoesNotHaveDefaultMediaTypeFormatter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingDefaultFormatters();
        }

        [Test]
        public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in WebApiController expected created result Formatters to be 4, but instead found 5.")]
        public void ContainingFormattersShouldThrowExceptionWhenActionResultHasDifferentCountOfMediaTypeFormatters()
        {
            var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();
            mediaTypeFormatters.Remove(mediaTypeFormatters.Last());

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(mediaTypeFormatters);
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in WebApiController expected created result Formatters to have CustomMediaTypeFormatter, but none was found.")]
        public void ContainingFormattersShouldThrowExceptionWhenActionResultHasDifferentTypeOfMediaTypeFormatters()
        {
            var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();
            mediaTypeFormatters.Remove(mediaTypeFormatters.Last());
            mediaTypeFormatters.Add(new CustomMediaTypeFormatter());

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(mediaTypeFormatters);
        }

        [Test]
        public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormattersAsParams()
        {
            var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(
                    mediaTypeFormatters[0],
                    mediaTypeFormatters[1],
                    mediaTypeFormatters[2],
                    mediaTypeFormatters[3],
                    mediaTypeFormatters[4]);
        }

        [Test]
        public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(
                    formatters => formatters
                        .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter())
                        .AndAlso()
                        .ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>());
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in WebApiController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        public void ContainingFormattersWithBuilderShouldThrowExceptionWhenActionResultHasIncorrectMediaTypeFormatters()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .ContainingMediaTypeFormatters(
                    formatters => formatters
                        .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>());
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedActionWithCustomContentNegotiator())
                .ShouldReturn()
                .Created()
                .AtLocation(TestObjectFactory.GetUri())
                .AndAlso()
                .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        [Test]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void AtShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.WithParameter(1));
        }

        [Test]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteVoidAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAtRouteAction action in WebApiController expected created result to redirect to '/api/Redirect/WithParameter?id=2', but in fact redirected to '/api/Redirect/WithParameter?id=1'.")]
        public void AtShouldThrowExceptionWithIncorrectActionParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.WithParameter(2));
        }

        [Test]
        [ExpectedException(
            typeof(CreatedResultAssertionException),
            ExpectedMessage = "When calling CreatedAtRouteAction action in WebApiController expected created result to redirect to a specific URI, but such URI could not be resolved from the 'Redirect' route template.")]
        public void AtShouldThrowExceptionWithIncorrectActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .At<RouteController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "Expected action result to contain a 'RouteName' property to test, but in fact such property was not found.")]
        public void AtShouldThrowExceptionWithIncorrectActionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .At<RouteController>(c => c.VoidAction());
        }
    }
}
