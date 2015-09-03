namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNullReferenceShouldThrowArgumentNullExceptionWithNullObject()
        {
            Validator.CheckForNullReference(null);
        }

        [Test]
        public void CheckForNullReferenceShouldNotThrowExceptionWithNotNullObject()
        {
            Validator.CheckForNullReference(new object());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithNullString()
        {
            Validator.CheckForNotWhiteSpaceString(null);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            Validator.CheckForNotWhiteSpaceString(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            Validator.CheckForNotWhiteSpaceString("      ");
        }

        [Test]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            Validator.CheckForNotWhiteSpaceString(new string('a', 10));
        }

        [Test]
        public void CheckForExceptionShouldNotThrowIfExceptionIsNull()
        {
            Validator.CheckForException(null);
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "NullReferenceException was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithEmptyMessage()
        {
            Validator.CheckForException(new NullReferenceException(string.Empty));
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test' message was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowIfExceptionIsNotNullWithMessage()
        {
            Validator.CheckForException(new NullReferenceException("Test"));
        }

        [Test]
        [ExpectedException(
            typeof(ActionCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown but was not caught or expected.")]
        public void CheckForExceptionShouldThrowWithProperMessageIfExceptionIsAggregateException()
        {
            var aggregateException = new AggregateException(new List<Exception>
                    {
                        new NullReferenceException("Null test"),
                        new InvalidCastException("Cast test"), 
                        new InvalidOperationException("Operation test")
                    });

            Validator.CheckForException(aggregateException);
        }
    }
}
