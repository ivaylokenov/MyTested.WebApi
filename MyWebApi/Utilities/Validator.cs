namespace MyWebApi.Utilities
{
    using System;

    public static class Validator
    {
        public static void CheckForNullReference(
            object value,
            string parameterName = "value",
            string errorMessageName = "Value")
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, string.Format("{0} cannot be null", errorMessageName));
            }
        }

        public static void CheckForNotEmptyString(
            string value,
            string parameterName = "value",
            string errorMessageName = "Value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName, string.Format("{0} cannot be null or empty", errorMessageName));
            }
        }
    }
}
