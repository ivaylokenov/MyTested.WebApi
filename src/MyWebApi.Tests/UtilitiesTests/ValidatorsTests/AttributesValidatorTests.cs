// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.UtilitiesTests.ValidatorsTests
{
    using System;
    using Builders;
    using Builders.Attributes;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Utilities.Validators;

    [TestFixture]
    public class AttributesValidatorTests
    {
        [Test]
        public void ValidateNoAttributesShouldNotFailWithNoAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new UserBuilder());

            AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "not have any attributes it had some")]
        public void ValidateNoAttributesShouldFailWithAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController());

            AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Test]
        public void ValidateAnyNumberOfAttributesShouldNotFailWithCorrectAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Test]
        public void ValidateAnyNumberOfAttributesShouldNotFailWithExpectedNumberOfAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres(), 2);
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "have at least 1 attribute in fact none was found")]
        public void ValidateAnyNumberOfAttributesShouldFailWithNoAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new UserBuilder());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "have 3 attributes in fact found 2")]
        public void ValidateAnyNumberOfAttributesShouldFailWithIncorrectExpectedNumberOfAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres(), 3);
        }

        [Test]
        public void ValidateAttributesShouldWorkCorrectly()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController());

            AttributesValidator.ValidateAttributes(
                attributes,
                new ActionAttributesTestBuilder(new WebApiController(), "Test"),
                TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }
    }
}
