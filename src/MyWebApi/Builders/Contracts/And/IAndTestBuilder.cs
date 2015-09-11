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

namespace MyWebApi.Builders.Contracts.And
{
    using Actions;
    using Base;

    /// <summary>
    /// Class containing AndAlso() method allowing additional assertions after model state tests.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IAndTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Method allowing additional assertions after the model state tests.
        /// </summary>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> AndAlso();
    }
}
