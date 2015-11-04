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

    /// <summary>
    /// Validator class containing HTTP response message validation logic.
    /// </summary>
    public static class HttpResponseMessageValidator
    {
        /// <summary>
        /// Tests whether the content of the HTTP response message is of certain type.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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

        /// <summary>
        /// Validates HTTP content for not null reference.
        /// </summary>
        /// <param name="content">HTTP content to validate.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
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

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and value.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
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

        /// <summary>
        /// Tests whether the HTTP response message contains response header with certain name and collection of value.
        /// </summary>
        /// <param name="headers">HTTP headers.</param>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeader">Indicates whether the header is content header.</param>
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

        /// <summary>
        /// Validates total number of HTTP headers.
        /// </summary>
        /// <param name="expectedHeaders">Expected HTTP headers.</param>
        /// <param name="actualHeaders">Actual HTTP headers.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="isContentHeaders">Indicates whether the headers are content headers.</param>
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

        /// <summary>
        /// Tests whether an object is returned from the invoked HTTP response message content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="failedResponseModelValidationAction">Func returning exception, in case of failed response model validation.</param>
        /// <returns>The actual HTTP response model.</returns>
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

        /// <summary>
        /// Gets the actual response model from HTTP content.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="content">Actual HTTP content.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>The actual HTTP response model.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided HttpStatusCode.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="statusCode">Expected status code.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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

        /// <summary>
        /// Tests whether HTTP response message version is the same as the provided version.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="version">Expected version.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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

        /// <summary>
        /// Tests whether HTTP response message reason phrase is the same as the provided reason phrase as string.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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

        /// <summary>
        /// Tests whether HTTP response message returns success status code between 200 and 299.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response message to test.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
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
