// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.HttpActionResults.InternalServerError;
    using HttpActionResults.InternalServerError;

    /// <summary>
    /// Class containing methods for testing InternalServerErrorResult or ExceptionResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is InternalServerErrorResult or ExceptionResult.
        /// </summary>
        /// <returns>Internal server error test builder.</returns>
        public IInternalServerErrorTestBuilder InternalServerError()
        {
            if (this.ActionResult as ExceptionResult != null)
            {
                return this.ReturnInternalServerErrorTestBuilder<ExceptionResult>();
            }

            return this.ReturnInternalServerErrorTestBuilder<InternalServerErrorResult>();
        }

        private IInternalServerErrorTestBuilder ReturnInternalServerErrorTestBuilder<TInternalServerErrorResult>()
            where TInternalServerErrorResult : class
        {
            var internalServerErrorResult = this.GetReturnObject<TInternalServerErrorResult>();
            return new InternalServerErrorTestBuilder<TInternalServerErrorResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                internalServerErrorResult);
        }
    }
}
