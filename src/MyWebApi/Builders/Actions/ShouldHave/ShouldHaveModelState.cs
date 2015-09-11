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

namespace MyWebApi.Builders.Actions.ShouldHave
{
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Models;
    using Exceptions;
    using Models;

    /// <summary>
    /// Class containing methods for testing model state.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Response model test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> ModelStateFor<TRequestModel>()
        {
            return new ModelErrorTestBuilder<TRequestModel>(this.Controller, this.ActionName, this.CaughtException);
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> ValidModelState()
        {
            this.CheckValidModelState();
            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        public IAndTestBuilder<TActionResult> InvalidModelState()
        {
            if (this.Controller.ModelState.Count == 0)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state, but was in fact valid.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return this.NewAndTestBuilder();
        }
    }
}
