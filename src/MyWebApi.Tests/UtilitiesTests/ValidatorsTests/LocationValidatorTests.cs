// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.UtilitiesTests.ValidatorsTests
{
    using System;
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Utilities.Validators;

    [TestFixture]
    public class LocationValidatorTests
    {
        [Test]
        public void ValidateAndGetWellFormedUriStringShouldReturnProperUriWithCorrectString()
        {
            string uriAsString = "http://somehost.com/someuri/1?query=Test";

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                uriAsString,
                TestObjectFactory.GetFailingValidationAction());

            Assert.IsNotNull(uri);
            Assert.AreEqual(uriAsString, uri.OriginalString);
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "location to be URI valid instead received http://somehost!@#?Query==true")]
        public void ValidateAndGetWellFormedUriStringShouldThrowExceptionWithIncorrectString()
        {
            string uriAsString = "http://somehost!@#?Query==true";

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                uriAsString,
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateUriShouldNotThrowExceptionWithProperUriWithCorrectString()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            LocationValidator.ValidateUri(
                actionResultWithLocation,
                TestObjectFactory.GetUri(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "location to be http://somehost.com/ instead received http://somehost.com/someuri/1?query=Test")]
        public void ValidateUriShouldThrowExceptionWithIncorrectString()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            LocationValidator.ValidateUri(
                actionResultWithLocation,
                new Uri("http://somehost.com/"),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateLocationShouldNotThrowExceptionWithCorrectLocationBuilder()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            LocationValidator.ValidateLocation(
                actionResultWithLocation,
                location =>
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
                        .WithQuery("?query=Test"),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "URI to equal the provided one was in fact different")]
        public void ValidateLocationShouldThrowExceptionWithIncorrectLocationBuilder()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            LocationValidator.ValidateLocation(
                actionResultWithLocation,
                location =>
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
                        .WithQuery("?query=Test"),
                TestObjectFactory.GetFailingValidationAction());
        }
    }
}
