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
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Exceptions;

    public static class HttpResponseMessageValidator
    {
        public static void WithContentOfType<TContentType>(
            HttpContent content,
            Action<string, string, string> failedValidationAction)
            where TContentType : HttpContent
        {
            var expectedType = typeof (TContentType);
            var actualType = content.GetType();
            if (Reflection.AreDifferentTypes(expectedType, actualType))
            {
                failedValidationAction(
                    "content",
                    string.Format("to be {0}", expectedType.ToFriendlyTypeName()),
                    string.Format("was in fact {0}", actualType.ToFriendlyTypeName()));
            }
        }

        public static void ValidateContent(HttpContent content, Action<string, string, string> failedValidationAction)
        {
            if (content == null)
            {
                failedValidationAction(
                    "content",
                    "to be initialized and set",
                    "it was null and no content headers were found");
            }
        }

        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            if (!headers.Contains(name))
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0}", name),
                    "none was found");
            }
        }

        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            string value,
            Action<string, string, string> failedValidationAction,
            bool isContentHeader = false)
        {
            ContainingHeader(headers, name, failedValidationAction);
            var headerValue = GetHeaderValues(headers, name).FirstOrDefault(v => v == value);

            if (headerValue == null)
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} value", name, value),
                    "none was found");
            }
        }

        public static void ContainingHeader(
            HttpHeaders headers,
            string name,
            IEnumerable<string> values,
            Action<string, string, string> failedValidationAction, 
            bool isContentHeader = false)
        {
            ContainingHeader(headers, name, failedValidationAction, isContentHeader);
            var actualHeaderValuesWithExpectedName = GetHeaderValues(headers, name);
            var expectedValues = values.ToList();
            var actualValuesCount = actualHeaderValuesWithExpectedName.Count;
            var expectedValuesCount = expectedValues.Count;
            if (expectedValuesCount != actualValuesCount)
            {
                failedValidationAction(
                    isContentHeader ? "content headers" : "headers",
                    string.Format("to contain {0} with {1} values", name, expectedValuesCount),
                    string.Format("instead found {0}", actualValuesCount));
            }

            var sortedActualValues = actualHeaderValuesWithExpectedName.OrderBy(v => v).ToList();
            var sortedExpectedValues = expectedValues.OrderBy(v => v).ToList();

            for (int i = 0; i < sortedActualValues.Count; i++)
            {
                var actualValue = sortedActualValues[i];
                var expectedValue = sortedExpectedValues[i];
                if (actualValue != expectedValue)
                {
                    failedValidationAction(
                        isContentHeader ? "content headers" : "headers",
                        string.Format("to have {0} with {1} value", name, expectedValue),
                        "none was found");
                }
            }
        }

        public static void ValidateHeadersCount(
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> expectedHeaders,
            HttpHeaders actualHeaders,
            Action<string, string, string> failedValidationAction,
            bool isContentHeaders = false)
        {
            var actualHeadersCount = actualHeaders.Count();
            var expectedHeadersCount = expectedHeaders.Count();

            if (expectedHeadersCount != actualHeadersCount)
            {
                failedValidationAction(
                    isContentHeaders ? "content headers" : "headers",
                    string.Format("to be {0}", expectedHeadersCount),
                    string.Format("were in fact {0}", actualHeadersCount));
            }
        }

        public static TResponseModel WithResponseModel<TResponseModel>(
            HttpContent content,
            TResponseModel expectedModel,
            Action<string, string, string> failedValidationAction,
            Func<string, string, ResponseModelAssertionException> failedResponseModelValidationAction)
            where TResponseModel : class
        {
            WithContentOfType<ObjectContent>(content, failedValidationAction);
            var actualModel = GetActualContentModel<TResponseModel>(
                content,
                failedResponseModelValidationAction);

            if (expectedModel != actualModel)
            {
                throw failedResponseModelValidationAction("be the given model", "in fact it was a different model");
            }

            return actualModel;
        }

        public static TResponseModel GetActualContentModel<TResponseModel>(
            HttpContent content,
            Func<string, string, ResponseModelAssertionException> failedValidationAction)
        {
            var responseModel = ((ObjectContent)content).Value;
            try
            {
                return (TResponseModel)responseModel;
            }
            catch (InvalidCastException)
            {
                throw failedValidationAction(
                   string.Format("be a {0}",typeof(TResponseModel).ToFriendlyTypeName()),
                   string.Format("instead received a {0}", responseModel.GetType().ToFriendlyTypeName()));
            }
        }

        public static void WithStatusCode(
            HttpResponseMessage httpResponseMessage,
            HttpStatusCode statusCode,
            Action<string, string, string> failedValidationAction)
        {
            var actualStatusCode = httpResponseMessage.StatusCode;
            if (actualStatusCode != statusCode)
            {
                failedValidationAction(
                    "status code",
                    string.Format("to be {0} ({1})", (int)statusCode, statusCode),
                    string.Format("instead received {0} ({1})", (int)actualStatusCode, actualStatusCode));
            }
        }

        public static void WithVersion(
            HttpResponseMessage httpResponseMessage,
            Version version,
            Action<string, string, string> failedValidationAction)
        {
            var actualVersion = httpResponseMessage.Version;
            if (actualVersion != version)
            {
                failedValidationAction(
                    "version",
                    string.Format("to be {0}", version),
                    string.Format("instead received {0}", actualVersion));
            }
        }

        public static void WithReasonPhrase(
            HttpResponseMessage httpResponseMessage,
            string reasonPhrase,
            Action<string, string, string> failedValidationAction)
        {
            var actualReasonPhrase = httpResponseMessage.ReasonPhrase;
            if (actualReasonPhrase != reasonPhrase)
            {
                failedValidationAction(
                    "reason phrase",
                    string.Format("to be '{0}'", reasonPhrase),
                    string.Format("instead received '{0}'", actualReasonPhrase));
            }
        }

        public static void WithSuccessStatusCode(HttpResponseMessage httpResponseMessage, Action<string, string, string> failedValidationAction)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                failedValidationAction(
                    "status code",
                    string.Format("to be between 200 and 299"),
                    string.Format("it was not"));
            }
        }

        private static IList<string> GetHeaderValues(HttpHeaders headers, string name)
        {
            return headers.First(h => h.Key == name).Value.ToList();
        }
    }
}
