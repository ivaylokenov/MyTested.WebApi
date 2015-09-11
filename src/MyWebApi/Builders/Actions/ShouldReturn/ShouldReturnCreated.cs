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

namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Created;
    using Created;
    using Utilities;

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
            var typeOfCreatedNegotiatedContentResult = typeof(CreatedNegotiatedContentResult<>);
            var typeOfCreatedAtRouteNegotiatedContentResult = typeof(CreatedAtRouteNegotiatedContentResult<>);
            var typeOfActionResult = this.ActionResult.GetType();
            if (!Reflection.AreAssignableByGeneric(typeOfCreatedNegotiatedContentResult, typeOfActionResult)
                && !Reflection.AreAssignableByGeneric(typeOfCreatedAtRouteNegotiatedContentResult, typeOfActionResult))
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                "CreatedNegotiatedContentResult<T> or CreatedAtRouteNegotiatedContentResult<T>",
                typeOfActionResult.ToFriendlyTypeName());
            }

            return new CreatedTestBuilder<TActionResult>(
                this.Controller,
                    this.ActionName,
                    this.CaughtException,
                    this.ActionResult);
        }
    }
}
