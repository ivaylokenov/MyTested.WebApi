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
            Validator.CheckForNotWhiteSpaceString(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            Validator.CheckForNotWhiteSpaceString(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            Validator.CheckForNotWhiteSpaceString("      ");
        }

        [Test]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            Validator.CheckForNotWhiteSpaceString(new string('a', 10));
        }
    }
}
