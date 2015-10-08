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
