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
    /// Exception for invalid status code result when expecting certain HTTP status code.
    /// </summary>
    public class HttpStatusCodeResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpStatusCodeResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpStatusCodeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
