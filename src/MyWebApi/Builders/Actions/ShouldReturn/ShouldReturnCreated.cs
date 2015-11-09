// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Created;
    using HttpActionResults.Created;

    /// <summary>
    /// Class containing methods for testing CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Created test builder.</returns>
        public ICreatedTestBuilder Created()
        {
            this.ValidateActionReturnType(typeof(CreatedNegotiatedContentResult<>), typeof(CreatedAtRouteNegotiatedContentResult<>));
            return new CreatedTestBuilder<TActionResult>(
                this.Controller,
                    this.ActionName,
                    this.CaughtException,
                    this.ActionResult);
        }
    }
}
