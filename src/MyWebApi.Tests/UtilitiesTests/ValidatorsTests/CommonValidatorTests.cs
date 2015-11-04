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
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Utilities.Validators;

    [TestFixture]
    public class CommonValidatorTests
    {
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNullReferenceShouldThrowArgumentNullExceptionWithNullObject()
        {
            CommonValidator.CheckForNullReference(null);
        }

        [Test]
        public void CheckForNullReferenceShouldNotThrowExceptionWithNotNullObject()
        {
            CommonValidator.CheckForNullReference(new object());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithNullString()
        {
            CommonValidator.CheckForNotWhiteSpaceString(null);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            CommonValidator.CheckForNotWhiteSpaceString(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            CommonValidator.CheckForNotWhiteSpaceString("      ");
        }

        [Test]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            CommonValidator.CheckForNotWhiteSpaceString(new string('a', 10));
        }

        [Test]
        public void CheckForExceptionShouldNotThrowIfExceptionIsNull()
        {
            CommonValidator.CheckForException(null);
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "NullReferenceException was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithEmptyMessage()
        {
            CommonValidator.CheckForException(new NullReferenceException(string.Empty));
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test' message was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithMessage()
        {
            CommonValidator.CheckForException(new NullReferenceException("Test"));
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowWithProperMessageIfExceptionIsAggregateException()
        {
            var aggregateException = new AggregateException(new List<Exception>
                    {
                        new NullReferenceException("Null test"),
                        new InvalidCastException("Cast test"), 
                        new InvalidOperationException("Operation test")
                    });

            CommonValidator.CheckForException(aggregateException);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForClass()
        {
            object obj = TestObjectFactory.GetNullRequestModel();
            var result = CommonValidator.CheckForDefaultValue(obj);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForStruct()
        {
            var result = CommonValidator.CheckForDefaultValue(0);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForNullableType()
        {
            var result = CommonValidator.CheckForDefaultValue<int?>(null);

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForClass()
        {
            object obj = TestObjectFactory.GetValidRequestModel();
            var result = CommonValidator.CheckForDefaultValue(obj);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForStruct()
        {
            var result = CommonValidator.CheckForDefaultValue(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithClass()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(object));
        }

        [Test]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithNullableType()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(int?));
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "Int32 cannot be null.")]
        public void CheckIfTypeCanBeNullShouldThrowExceptionWithStruct()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(int));
        }
    }
}
