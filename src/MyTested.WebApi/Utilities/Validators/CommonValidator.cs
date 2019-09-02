// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;
    using Exceptions;

    /// <summary>
    /// Validator class containing common validation logic.
    /// </summary>
    public static class CommonValidator
    {
        /// <summary>
        /// Validates object for null reference.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNullReference(
            object value,
            string errorMessageName = "Value")
        {
            if (value == null)
            {
                throw new NullReferenceException(string.Format("{0} cannot be null.", errorMessageName));
            }
        }

        /// <summary>
        /// Validates string for null reference or whitespace.
        /// </summary>
        /// <param name="value">String to be validated.</param>
        /// <param name="errorMessageName">Name of the parameter to be included in the error message.</param>
        public static void CheckForNotWhiteSpaceString(
            string value,
            string errorMessageName = "Value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullReferenceException(string.Format("{0} cannot be null or white space.", errorMessageName));
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
            if (CheckForDefaultValue(value))
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        /// <summary>
        /// Validated whether a non-null exception is provided and throws ActionCallAssertionException with proper message.
        /// </summary>
        /// <param name="exception">Exception to be validated.</param>
        public static void CheckForException(Exception exception)
        {
            if (exception != null)
            {
                var message = FormatExceptionMessage(exception.Message);

                var exceptionAsAggregateException = exception as AggregateException;
                if (exceptionAsAggregateException != null)
                {
                    var innerExceptions = exceptionAsAggregateException
                        .InnerExceptions
                        .Select(ex =>
                            string.Format("{0}{1}", ex.GetName(), FormatExceptionMessage(ex.Message)));

                    message = string.Format(" (containing {0})", string.Join(", ", innerExceptions));
                }
                else
                {
                    if (exception.InnerException != null)
                    {
                        message = string.Format("{0}{1}(containing {2}{3})", message, Environment.NewLine, exception.InnerException.GetName(), FormatExceptionMessage(exception.InnerException.Message));
                    }
                }

                throw new InvalidCallAssertionException(string.Format(
                    "{0}{1} was thrown but was not caught or expected.{2}",
                    exception.GetType().ToFriendlyTypeName(),
                    message,
                    exception.StackTrace));
            }
        }

        /// <summary>
        /// Validates that two objects are equal using the Equals method.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <returns>True or false.</returns>
        public static bool CheckEquality<T>(T expected, T actual)
        {
            return expected.Equals(actual);
        }

        /// <summary>
        /// Validates whether object equals the default value for its type.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="value">Object to test.</param>
        /// <returns>True or false.</returns>
        public static bool CheckForDefaultValue<TValue>(TValue value)
        {
            return EqualityComparer<TValue>.Default.Equals(value, default(TValue));
        }

        /// <summary>
        /// Validates whether type can be null.
        /// </summary>
        /// <param name="type">Type to check.</param>
        public static void CheckIfTypeCanBeNull(Type type)
        {
            bool canBeNull = !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);
            if (!canBeNull)
            {
                throw new InvalidCallAssertionException(string.Format(
                    "{0} cannot be null.",
                    type.ToFriendlyTypeName()));
            }
        }

        private static string FormatExceptionMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message)
                 ? string.Empty
                 : string.Format(" with '{0}' message", message);
        }
    }
}
