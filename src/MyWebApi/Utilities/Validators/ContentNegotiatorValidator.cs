// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Utilities.Validators
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
