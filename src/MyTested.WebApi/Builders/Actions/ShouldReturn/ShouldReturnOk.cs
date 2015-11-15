// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Ok;
    using HttpActionResults.Ok;

    /// <summary>
    /// Class containing methods for testing OkResult and OkNegotiatedContentResult{T}.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is plain OkResult.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        public IOkTestBuilder Ok()
        {
            var actionResultAsOkResult = this.ActionResult as OkResult;
            if (actionResultAsOkResult != null)
            {
                this.ResultOfType<OkResult>();
            }
            else
            {
                this.ValidateActionReturnType(typeof(OkNegotiatedContentResult<>), allowDifferentGenericTypeDefinitions: true);
            }

            return new OkTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
