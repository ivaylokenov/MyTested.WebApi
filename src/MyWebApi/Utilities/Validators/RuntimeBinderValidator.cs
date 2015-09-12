namespace MyWebApi.Utilities.Validators
{
    using System;
    using Exceptions;
    using Microsoft.CSharp.RuntimeBinder;

    public static class RuntimeBinderValidator
    {
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
