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

namespace MyWebApi.Builders.Contracts.Controllers
{
    using System;
    using Attributes;
    using Base;

    /// <summary>
    /// Used for testing controllers.
    /// </summary>
    public interface IControllerTestBuilder
    {
        /// <summary>
        /// Checks whether the tested controller has no attributes of any type. 
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder NoActionAttributes();

        /// <summary>
        /// Checks whether the tested controller has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested controller.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder ActionAttributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Checks whether the tested controller has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the controller.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder ActionAttributes(Action<IControllerAttributesTestBuilder> attributesTestBuilder);
    }
}
