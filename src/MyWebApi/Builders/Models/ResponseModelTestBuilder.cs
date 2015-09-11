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

namespace MyWebApi.Builders.Models
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.Models;
    using Exceptions;

    /// <summary>
    /// Used for testing the response model type of action.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelTestBuilder<TActionResult>
        : BaseResponseModelTestBuilder<TActionResult>, IResponseModelTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ResponseModelTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder WithNoResponseModel()
        {
            var actualResult = this.ActionResult as OkResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetName()));
            }

            return this.NewAndProvideTestBuilder();
        }
    }
}
