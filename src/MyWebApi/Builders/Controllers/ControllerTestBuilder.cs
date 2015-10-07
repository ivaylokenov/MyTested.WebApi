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
    using Base;
    using Common.Extensions;
    using Contracts.Attributes;
    using Contracts.Base;
    using Contracts.Controllers;
    using Exceptions;
    using Utilities.Validators;

    public class ControllerTestBuilder : BaseTestBuilder, IControllerTestBuilder
    {
        public ControllerTestBuilder(
            ApiController controller,
            IEnumerable<object> controllerAttributes)
            : base(controller, controllerAttributes)
        {
        }

        public IBaseTestBuilder NoActionAttributes()
        {
            AttributesValidator.ValidateNoAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException);

            return this;
        }

        public IBaseTestBuilder ActionAttributes(int? withTotalNumberOf = null)
        {
            AttributesValidator.ValidateNumberOfAttributes(
                this.ControllerLevelAttributes,
                this.ThrowNewAttributeAssertionException,
                withTotalNumberOf);

            return this;
        }

        public IBaseTestBuilder ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder)
        {
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
