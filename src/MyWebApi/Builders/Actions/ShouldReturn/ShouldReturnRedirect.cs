// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Redirect;
    using HttpActionResults.Redirect;

    /// <summary>
    /// Class containing methods for testing RedirectResult or RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is RedirectResult or RedirectToRouteResult.
        /// </summary>
        /// <returns>Redirect test builder.</returns>
        public IRedirectTestBuilder Redirect()
        {
            var actionResultAsRedirectResult = this.ActionResult as RedirectToRouteResult;
            if (actionResultAsRedirectResult != null)
            {
                return this.ReturnRedirectTestBuilder<RedirectToRouteResult>();
            }

            return this.ReturnRedirectTestBuilder<RedirectResult>();
        }

        private IRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
            where TRedirectResult : class
        {
            var redirectResult = this.GetReturnObject<TRedirectResult>();
            return new RedirectTestBuilder<TRedirectResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                redirectResult);
        }
    }
}
