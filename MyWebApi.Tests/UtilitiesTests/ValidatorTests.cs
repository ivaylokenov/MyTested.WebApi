namespace MyWebApi.Tests.UtilitiesTests
{
    using System;

    using NUnit.Framework;

    using Utilities;

    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithNullString()
        {
            Validator.CheckForNotEmptyString(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            Validator.CheckForNotEmptyString(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            Validator.CheckForNotEmptyString("      ");
        }

        [Test]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            Validator.CheckForNotEmptyString(new string('a', 10));
        }
    }
}
