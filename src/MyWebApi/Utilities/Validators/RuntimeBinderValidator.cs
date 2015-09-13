namespace MyWebApi.Utilities.Validators
{
    using System;
    using Exceptions;
    using Microsoft.CSharp.RuntimeBinder;

    /// <summary>
    /// Validator class containing dynamic action result calls validation logic.
    /// </summary>
    public static class RuntimeBinderValidator
    {
        /// <summary>
        /// Validates action call for RuntimeBinderException.
        /// </summary>
        /// <param name="action">Action to validate.</param>
        public static void ValidateBinding(Action action)
        {
            try
            {
                action();
            }
            catch (RuntimeBinderException)
            {
                throw new ActionCallAssertionException("Expected action result to contain a property to test, but in fact such property was not found.");
            }
        }
    }
}
