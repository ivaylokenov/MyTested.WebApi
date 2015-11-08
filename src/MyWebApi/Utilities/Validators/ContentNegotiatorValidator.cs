// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Utilities.Validators
{
    using System;
    using System.Net.Http.Formatting;
    using Common.Extensions;

    /// <summary>
    /// Validator class containing IContentNegotiator validation logic.
    /// </summary>
    public static class ContentNegotiatorValidator
    {
        /// <summary>
        /// Validates the IContentNegotiator from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with IContentNegotiator.</param>
        /// <param name="contentNegotiator">Expected IContentNegotiator.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentNegotiator(
            dynamic actionResult,
            IContentNegotiator contentNegotiator,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualContentNegotiator = actionResult.ContentNegotiator as IContentNegotiator;
                if (Reflection.AreDifferentTypes(contentNegotiator, actualContentNegotiator))
                {
                    failedValidationAction(
                        "IContentNegotiator",
                        string.Format("to be {0}", contentNegotiator.GetName()),
                        string.Format("instead received {0}", actualContentNegotiator.GetName()));
                }
            });
        }
    }
}
