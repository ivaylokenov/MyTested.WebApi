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
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Utilities;
    using Utilities.Validators;

    [TestFixture]
    public class MediaTypeFormatterValidatorTests
    {
        [Test]
        public void GetDefaultMediaTypeFormattersShouldReturnProperFormatters()
        {
            var defaultFormatters = MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters();

            Assert.IsNotNull(defaultFormatters);

            var result = defaultFormatters
                .All(f => Reflection.AreSameTypes(f.GetType(), typeof (FormUrlEncodedMediaTypeFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof (JQueryMvcFormUrlEncodedFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof(JsonMediaTypeFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof(XmlMediaTypeFormatter)));

            Assert.AreEqual(4, defaultFormatters.Count());
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                actionResultWithFormatters,
                new FormUrlEncodedMediaTypeFormatter(), 
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "Formatters to contain CustomMediaTypeFormatter none was found")]
        public void ValidateMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                actionResultWithFormatters,
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateMediaTypeFormattersShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
                actionResultWithFormatters,
                MediaTypeFormatterValidator.GetDefaultMediaTypeFormatters(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "Formatters to be 5 instead found 4")]
        public void ValidateMediaTypeFormattersShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
                actionResultWithFormatters,
                TestObjectFactory.GetFormatters(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateMediaTypeFormattersBuilderShouldNotThrowExceptionWithCorrectBuilder()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
                actionResultWithFormatters,
                formatters => formatters
                        .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter())
                        .AndAlso()
                        .ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "Formatters to contain CustomMediaTypeFormatter none was found")]
        public void ValidateMediaTypeFormattersBuilderShouldThrowExceptionWithIncorrectBuilder()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().Controller);

            MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
                actionResultWithFormatters,
                formatters => formatters
                        .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>(),
                TestObjectFactory.GetFailingValidationAction());
        }
    }
}
