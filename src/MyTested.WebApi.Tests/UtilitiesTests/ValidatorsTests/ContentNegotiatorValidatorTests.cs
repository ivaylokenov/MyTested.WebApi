// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.UtilitiesTests.ValidatorsTests
{
    using System;
    using System.Net.Http.Formatting;
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Utilities.Validators;

    [TestFixture]
    public class ContentNegotiatorValidatorTests
    {
        [Test]
        public void ValidateContentNegotiatorShouldNotThrowExceptionIfContentValidatorIsCorrect()
        {
            var actionResultWithContentNegotiator = new OkNegotiatedContentResult<int>(5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            ContentNegotiatorValidator.ValidateContentNegotiator(
                actionResultWithContentNegotiator,
                new DefaultContentNegotiator(),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "IContentNegotiator to be CustomContentNegotiator instead received DefaultContentNegotiator")]
        public void ValidateContentNegotiatorShouldThrowExceptionIfContentValidatorIsNotCorrect()
        {
            var actionResultWithContentNegotiator = new OkNegotiatedContentResult<int>(5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

            ContentNegotiatorValidator.ValidateContentNegotiator(
                actionResultWithContentNegotiator,
                new CustomContentNegotiator(),
                TestObjectFactory.GetFailingValidationAction());
        }
    }
}
