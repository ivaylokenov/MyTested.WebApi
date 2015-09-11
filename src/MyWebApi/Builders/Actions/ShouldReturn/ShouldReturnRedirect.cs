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
    using Contracts.Redirect;
    using Redirect;

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
