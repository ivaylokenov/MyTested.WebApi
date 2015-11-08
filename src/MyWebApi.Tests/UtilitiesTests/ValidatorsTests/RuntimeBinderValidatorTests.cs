// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
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
