// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Utilities.Validators
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
            catch (RuntimeBinderException ex)
            {
                var fullPropertyName = ex.Message.Split('\'')[3];
                throw new InvalidCallAssertionException(string.Format(
                    "Expected action result to contain a '{0}' property to test, but in fact such property was not found.",
                    fullPropertyName));
            }
        }
    }
}
