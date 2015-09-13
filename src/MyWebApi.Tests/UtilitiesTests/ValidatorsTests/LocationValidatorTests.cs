// MyWebApi - ASP.NET Web API Fluent Testing Framework
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
            const string uriAsString = "http://somehost.com/someuri/1?query=Test";

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
            const string uriAsString = "http://somehost!@#?Query==true";

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                uriAsString,
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateUriShouldNotThrowExceptionWithProperUriWithCorrectString()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

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
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            LocationValidator.ValidateUri(
                actionResultWithLocation,
                new Uri("http://somehost.com/"),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateLocationShouldNotThrowExceptionWithCorrectLocationBuilder()
        {
            var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

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
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

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
