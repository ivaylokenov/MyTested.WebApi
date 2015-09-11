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

namespace MyWebApi.Builders.Contracts.Actions
{
    using Base;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Used for testing action attributes and model state.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        IShouldHaveTestBuilder<TActionResult> ShouldHave();

        /// <summary>
        /// Used for testing whether action throws exception.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        IShouldThrowTestBuilder ShouldThrow();

        /// <summary>
        /// Used for testing returned action result.
        /// </summary>
        /// <returns>Should return test builder.</returns>
        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}
