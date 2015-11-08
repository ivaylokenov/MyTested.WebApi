// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Content;
    using HttpActionResults.Content;

    /// <summary>
    /// Class containing methods for testing NegotiatedContentResult{T} or FormattedContentResult{T}.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is NegotiatedContentResult{T} or FormattedContentResult{T}.
        /// </summary>
        /// <returns>Content test builder.</returns>
        public IContentTestBuilder Content()
        {
            this.ValidateActionReturnType(typeof(NegotiatedContentResult<>), typeof(FormattedContentResult<>));
            return new ContentTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
