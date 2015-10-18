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

namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid route test.
    /// </summary>
    public class RouteAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RouteAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public RouteAssertionException(string message)
            : base(message)
        {
        }
    }
}
