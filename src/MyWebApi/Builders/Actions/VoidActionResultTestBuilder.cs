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

namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Base;
    using Common;
    using Contracts.Actions;
    using Contracts.Base;
    using ShouldHave;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public class VoidActionResultTestBuilder : BaseTestBuilderWithCaughtException, IVoidActionResultTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidActionResultTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        public VoidActionResultTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionAttributes)
        {
        }

        /// <summary>
        /// Tests whether action result is void.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException ShouldReturnEmpty()
        {
            CommonValidator.CheckForException(this.CaughtException);
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Used for testing action attributes and model state.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        public IShouldHaveTestBuilder<VoidActionResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<VoidActionResult>(
                this.Controller, 
                this.ActionName, 
                this.CaughtException, 
                VoidActionResult.Create(),
                this.ActionLevelAttributes);
        }

        /// <summary>
        /// Used for testing whether action throws exception.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
