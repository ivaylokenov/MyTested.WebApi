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

namespace MyWebApi.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Attributes;
    using Base;
    using Common.Extensions;
    using Contracts.Attributes;
    using Contracts.Base;
    using Contracts.Controllers;
    using Exceptions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing controllers.
    /// </summary>
    public class ControllerTestBuilder : BaseTestBuilder, IControllerTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller which will be tested.</param>
        /// <param name="controllerAttributes">Collected attributes from the tested controller.</param>
        public ControllerTestBuilder(
            ApiController controller,
            IEnumerable<object> controllerAttributes)
            : base(controller, controllerAttributes)
        {
        }

        /// <summary>
        /// Checks whether the tested controller has no attributes of any type. 
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        /// <summary>
        /// Checks whether the tested controller has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested controller.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this;
        }

        /// <summary>
        /// Checks whether the tested controller has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the controller.</param>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder ActionAttributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder)
        {
            var newAttributesTestBuilder = new ControllerAttributesTestBuilder(this.Controller);
            attributesTestBuilder(newAttributesTestBuilder);

            AttributesValidator.ValidateAttributes(
                this.ControllerLevelAttributes,
                newAttributesTestBuilder,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} controller expected to {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
