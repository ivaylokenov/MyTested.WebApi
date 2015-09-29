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

namespace MyWebApi.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using And;
    using Contracts.Base;

    /// <summary>
    /// Base class for test builders with caught exception.
    /// </summary>
    public class BaseTestBuilderWithCaughtException
        : BaseTestBuilder, IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithCaughtException" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithCaughtException(
            ApiController controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes = null)
            : base(controller, actionName, actionAttributes)
        {
            this.CaughtException = caughtException;
        }

        internal Exception CaughtException { get; private set; }

        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        public Exception AndProvideTheCaughtException()
        {
            return this.CaughtException;
        }

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder.</returns>
        protected IBaseTestBuilderWithCaughtException NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
