// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.UtilitiesTests.ValidatorsTests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
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
                .All(f => Reflection.AreSameTypes(f.GetType(), typeof(FormUrlEncodedMediaTypeFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof(JQueryMvcFormUrlEncodedFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof(JsonMediaTypeFormatter))
                          || Reflection.AreSameTypes(f.GetType(), typeof(XmlMediaTypeFormatter)));

            Assert.AreEqual(4, defaultFormatters.Count());
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateMediaTypeFormatterShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(),
                5,
                MyWebApi.Controller<WebApiController>().AndProvideTheController());

            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                actionResultWithFormatters,
                new FormUrlEncodedMediaTypeFormatter(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateMediaTypeFormatterShouldNotThrowExceptionWithSingleCorrectMediaTypeFormatter()
        {
            var actionResultWithFormatter = new FormattedContentResult<int>(
                HttpStatusCode.OK,
                5,
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                new MediaTypeHeaderValue(TestObjectFactory.MediaType),
                MyWebApi.Controller<WebApiController>().AndProvideTheController());

            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                actionResultWithFormatter,
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
           typeof(NullReferenceException),
           ExpectedMessage = "Formatters to contain CustomMediaTypeFormatter none was found")]
        public void ValidateMediaTypeFormatterShouldThrowExceptionWithIncorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                actionResultWithFormatters,
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateMediaTypeFormattersShouldNotThrowExceptionWithCorrectMediaTypeFormatter()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

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
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            MediaTypeFormatterValidator.ValidateMediaTypeFormatters(
                actionResultWithFormatters,
                TestObjectFactory.GetFormatters(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        public void ValidateMediaTypeFormattersBuilderShouldNotThrowExceptionWithCorrectBuilder()
        {
            var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

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
                TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            MediaTypeFormatterValidator.ValidateMediaTypeFormattersBuilder(
                actionResultWithFormatters,
                formatters => formatters
                        .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>(),
                TestObjectFactory.GetFailingValidationAction());
        }
    }
}
