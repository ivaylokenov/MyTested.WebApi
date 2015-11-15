// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http.ModelBinding;
    using Builders;
    using Builders.Contracts.Formatters;
    using Common.Extensions;
    using Microsoft.CSharp.RuntimeBinder;

    /// <summary>
    /// Validator class containing MediaTypeFormatter validation logic.
    /// </summary>
    public static class MediaTypeFormatterValidator
    {
        /// <summary>
        /// Returns default media type formatters used in ASP.NET Web API.
        /// </summary>
        /// <returns>Enumerable of MediaTypeFormatter.</returns>
        public static IEnumerable<MediaTypeFormatter> GetDefaultMediaTypeFormatters()
        {
            return new List<MediaTypeFormatter>
            {
                new FormUrlEncodedMediaTypeFormatter(),
                new JQueryMvcFormUrlEncodedFormatter(),
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            };
        }

        /// <summary>
        /// Validates the Formatters from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with Formatters.</param>
        /// <param name="mediaTypeFormatter">Expected MediaTypeFormatter.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateMediaTypeFormatter(
            dynamic actionResult,
            MediaTypeFormatter mediaTypeFormatter,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var formatters = TryGetMediaTypeFormatters(actionResult) as IEnumerable<MediaTypeFormatter>;
                if (formatters == null || formatters.All(f => Reflection.AreDifferentTypes(f, mediaTypeFormatter)))
                {
                    failedValidationAction(
                        "Formatters",
                        string.Format("to contain {0}", mediaTypeFormatter.GetName()),
                        "none was found");
                }
            });
        }

        /// <summary>
        /// Validates the Formatters from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with Formatters.</param>
        /// <param name="mediaTypeFormatters">Expected enumerable of MediaTypeFormatter.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateMediaTypeFormatters(
            dynamic actionResult,
            IEnumerable<MediaTypeFormatter> mediaTypeFormatters,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var formatters = TryGetMediaTypeFormatters(actionResult) as IEnumerable<MediaTypeFormatter>;
                var actualMediaTypeFormatters = SortMediaTypeFormatters(formatters);
                var expectedMediaTypeFormatters = SortMediaTypeFormatters(mediaTypeFormatters);

                if (actualMediaTypeFormatters.Count != expectedMediaTypeFormatters.Count)
                {
                    failedValidationAction(
                        "Formatters",
                        string.Format("to be {0}", expectedMediaTypeFormatters.Count),
                        string.Format("instead found {0}", actualMediaTypeFormatters.Count));
                }

                for (int i = 0; i < actualMediaTypeFormatters.Count; i++)
                {
                    var actualMediaTypeFormatter = actualMediaTypeFormatters[i];
                    var expectedMediaTypeFormatter = expectedMediaTypeFormatters[i];
                    if (actualMediaTypeFormatter != expectedMediaTypeFormatter)
                    {
                        failedValidationAction(
                            "Formatters",
                            string.Format("to have {0}", expectedMediaTypeFormatters[i]),
                            "none was found");
                    }
                }
            });
        }

        /// <summary>
        /// Validates the Formatters from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with Formatters.</param>
        /// <param name="formattersBuilder">Formatters builder.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateMediaTypeFormattersBuilder(
            dynamic actionResult,
            Action<IFormattersBuilder> formattersBuilder,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var newFormattersBuilder = new FormattersBuilder();
                formattersBuilder(newFormattersBuilder);
                var expectedFormatters = newFormattersBuilder.GetMediaTypeFormatters();
                expectedFormatters.ForEach(formatter =>
                    ValidateMediaTypeFormatter(
                        actionResult,
                        formatter,
                        failedValidationAction));
            });
        }

        private static IEnumerable<MediaTypeFormatter> TryGetMediaTypeFormatters(dynamic actionResult)
        {
            IEnumerable<MediaTypeFormatter> formatters = new List<MediaTypeFormatter>();

            try
            {
                var formatter = actionResult.Formatter as MediaTypeFormatter;
                formatters = new List<MediaTypeFormatter> { formatter };
            }
            catch (RuntimeBinderException)
            {
                RuntimeBinderValidator.ValidateBinding(() =>
                {
                    formatters = actionResult.Formatters as IEnumerable<MediaTypeFormatter>;
                });
            }

            return formatters;
        }

        private static IList<string> SortMediaTypeFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
        {
            return mediaTypeFormatters
                .OrderBy(m => m.GetType().FullName)
                .Select(m => m.GetType().Name)
                .ToList();
        }
    }
}
