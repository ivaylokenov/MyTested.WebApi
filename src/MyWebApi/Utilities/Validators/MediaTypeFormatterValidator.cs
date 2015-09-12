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
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http.ModelBinding;
    using Builders;
    using Builders.Contracts.Formatters;
    using Common.Extensions;

    public static class MediaTypeFormatterValidator
    {
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

        public static void ValidateMediaTypeFormatter(
            dynamic actionResult,
            MediaTypeFormatter mediaTypeFormatter,
            Action<string, string, string> failedValidationAction)
        {
            var formatters = actionResult.Formatters as IEnumerable<MediaTypeFormatter>;
            if (formatters == null || formatters.All(f => Reflection.AreDifferentTypes(f, mediaTypeFormatter)))
            {
                failedValidationAction(
                    "Formatters",
                    string.Format("to contain {0}", mediaTypeFormatter.GetName()),
                    "none was found");
            }
        }

        public static void ValidateMediaTypeFormatters(
            dynamic actionResult,
            IEnumerable<MediaTypeFormatter> mediaTypeFormatters,
            Action<string, string, string> failedValidationAction)
        {
            var formatters = actionResult.Formatters as IEnumerable<MediaTypeFormatter>;
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
        }

        public static void ValidateMediaTypeFormattersBuilder(
            dynamic actionResult,
            Action<IFormattersBuilder> formattersBuilder,
            Action<string, string, string> failedValidationAction)
        {
            var newFormattersBuilder = new FormattersBuilder();
            formattersBuilder(newFormattersBuilder);
            var expectedFormatters = newFormattersBuilder.GetMediaTypeFormatters();
            expectedFormatters.ForEach(formatter =>
                ValidateMediaTypeFormatter(
                    actionResult,
                    formatter,
                    failedValidationAction));
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
