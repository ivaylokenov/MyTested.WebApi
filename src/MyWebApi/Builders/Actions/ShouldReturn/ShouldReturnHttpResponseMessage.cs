// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Actions.ShouldReturn
{
    using System.Net.Http;
    using Contracts.HttpResponseMessages;
    using HttpMessages;

    /// <summary>
    /// Class containing methods for testing HttpResponseMessage result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        public IHttpResponseMessageTestBuilder HttpResponseMessage()
        {
            this.ResultOfType<HttpResponseMessage>();
            return new HttpResponseMessageTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as HttpResponseMessage);
        }
    }
}
