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

namespace MyWebApi.Builders.And
{
    using System;
    using System.Web.Http;
    using Actions;
    using Base;
    using Contracts.Actions;
    using Contracts.And;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after model state tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class AndTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>,
        IAndTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public AndTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Method allowing additional assertions after the model state tests.
        /// </summary>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> AndAlso()
        {
            return new ActionResultTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
