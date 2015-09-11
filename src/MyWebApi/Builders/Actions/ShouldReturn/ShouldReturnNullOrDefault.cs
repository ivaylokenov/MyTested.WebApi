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
    using Common.Extensions;
    using Contracts.Base;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Class containing methods for testing null or default value result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> DefaultValue()
        {
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "the default value of {0}, but in fact it was not.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Null()
        {
            Validator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (!this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "null, but instead received {0}.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is not null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotNull()
        {
            Validator.CheckIfTypeCanBeNull(typeof(TActionResult));
            if (this.CheckValidDefaultValue())
            {
                this.ThrowNewHttpActionResultAssertionException(string.Format(
                    "not null, but it was {0} object.",
                    typeof(TActionResult).ToFriendlyTypeName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        private bool CheckValidDefaultValue()
        {
            return Validator.CheckForDefaultValue(this.ActionResult) && this.CaughtException == null;
        }

        private void ThrowNewHttpActionResultAssertionException(string message)
        {
            throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}",
                    this.ActionName,
                    this.Controller.GetName(),
                    message));
        }
    }
}
