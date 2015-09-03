namespace MyWebApi.Utilities
{
    using System;
    using Exceptions;

    /// <summary>
    /// Validator class containing common validation logic.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates object for null reference.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="parameterName">Name of the parameter to be checked.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNullReference(
            object value,
            string parameterName = "value",
            string errorMessageName = "Value")
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, string.Format("{0} cannot be null.", errorMessageName));
            }
        }

        /// <summary>
        /// Validates string for null reference or whitespace.
        /// </summary>
        /// <param name="value">String to be validated.</param>
        /// <param name="parameterName">Name of the parameter to be checked.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNotWhiteSpaceString(
            string value,
            string parameterName = "value",
            string errorMessageName = "Value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName, string.Format("{0} cannot be null or white space.", errorMessageName));
            }
        }

        /// <summary>
        /// Validates whether the provided value is not null or equal to the type's default value.
        /// </summary>
        /// <typeparam name="T">Type of the provided value.</typeparam>
        /// <param name="value">Value to be validated.</param>
        /// <param name="errorMessage">Error message if the validation fails.</param>
        public static void CheckForEqualityWithDefaultValue<T>(T value, string errorMessage)
        {
            if (value == null || value.Equals(default(T)))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        public static void CheckForException(Exception exception)
        {
            if (exception != null)
            {
                var message = 
                    string.IsNullOrWhiteSpace(exception.Message) 
                    ? string.Empty 
                    : string.Format(" with '{0}' message", exception.Message);

                throw new ActionCallAssertionException(string.Format(
                    "{0}{1} was thrown but was not caught or expected.",
                    exception.GetType().ToFriendlyTypeName(),
                    message));
            }
        }
    }
}
