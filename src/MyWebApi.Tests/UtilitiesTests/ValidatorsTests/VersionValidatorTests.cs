// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.UtilitiesTests.ValidatorsTests
{
    using System;
    using NUnit.Framework;
    using Setups;
    using Utilities.Validators;

    [TestFixture]
    public class VersionValidatorTests
    {
        [Test]
        public void TryParseShouldReturnCorrectVersion()
        {
            var version = VersionValidator.TryParse("1.1", TestObjectFactory.GetFailingValidationAction());

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(1, version.Minor);
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "version valid version string invalid one")]
        public void TryParseShouldInvokeFailedActionIfStringIsNotInCorrectFormat()
        {
            VersionValidator.TryParse("test", TestObjectFactory.GetFailingValidationAction());
        }
    }
}
