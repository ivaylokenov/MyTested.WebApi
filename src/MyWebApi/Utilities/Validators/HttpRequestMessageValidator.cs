// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Utilities.Validators
{
    using System.Net.Http;
    using Exceptions;

    /// <summary>
    /// Validator class containing HTTP request message validations.
    /// </summary>
    public static class HttpRequestMessageValidator
    {
        /// <summary>
        /// Checks whether content headers can be added to a HttpRequestMessage.
        /// </summary>
        /// <param name="requestMessage">HttpRequestMessage to validate.</param>
        public static void ValidateContent(HttpRequestMessage requestMessage)
        {
            if (requestMessage.Content == null)
            {
                throw new InvalidHttpRequestMessageException("When building HttpRequestMessage expected content to be initialized and set in order to add content headers.");
            }
        }
    }
}
