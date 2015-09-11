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

namespace MyWebApi.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing specific exception messages.
    /// </summary>
    public interface IExceptionMessageTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether particular exception message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular exception.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular exception message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular exception message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular exception message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder Containing(string containsMessage);
    }
}
