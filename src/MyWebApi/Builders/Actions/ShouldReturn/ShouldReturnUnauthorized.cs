// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.Unauthorized;
    using HttpActionResults.Unauthorized;

    /// <summary>
    /// Class containing methods for testing UnauthorizedResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        public IUnauthorizedTestBuilder Unauthorized()
        {
            var unathorizedResult = this.GetReturnObject<UnauthorizedResult>();
            return new UnauthorizedTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                unathorizedResult);
        }
    }
}
