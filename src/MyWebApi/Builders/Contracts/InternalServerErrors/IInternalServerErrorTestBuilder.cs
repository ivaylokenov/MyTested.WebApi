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

namespace MyWebApi.Builders.Contracts.InternalServerErrors
{
    using System;
    using Base;
    using ExceptionErrors;

    /// <summary>
    /// Used for testing internal server error results.
    /// </summary>
    public interface IInternalServerErrorTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests internal server error whether it contains exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        IExceptionTestBuilder WithException();

        /// <summary>
        /// Tests internal server error whether it contains exception with the same type and having the same message as the provided exception.
        /// </summary>
        /// <param name="exception">Expected exception.</param>
        /// <returns>Exception test builder.</returns>
        IBaseTestBuilder WithException(Exception exception);
    }
}
