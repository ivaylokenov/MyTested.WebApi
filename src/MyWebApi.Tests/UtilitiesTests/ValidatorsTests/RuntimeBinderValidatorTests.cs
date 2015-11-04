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
    using System.Web.Http.Results;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Utilities.Validators;

    [TestFixture]
    public class RuntimeBinderValidatorTests
    {
        [Test]
        public void ValidateBindingShouldNotThrowExceptionWithValidPropertyCall()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var contentNegotiator = (actionResultWithFormatters as dynamic).ContentNegotiator;
                Assert.IsNotNull(contentNegotiator);
            });
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "Expected action result to contain a 'ModelState' property to test, but in fact such property was not found.")]
        public void ValidateBindingShouldThrowExceptionWithInvalidPropertyCall()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var contentNegotiator = (actionResultWithFormatters as dynamic).ModelState;
                Assert.IsNotNull(contentNegotiator);
            });
        }
    }
}
