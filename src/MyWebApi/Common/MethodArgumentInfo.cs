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

namespace MyWebApi.Common
{
    using System;

    /// <summary>
    /// Method argument information containing name, type and value for an method parameter.
    /// </summary>
    public class MethodArgumentInfo
    {
        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        /// <value>Argument's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        /// <value>Argument's type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the argument.
        /// </summary>
        /// <value>Arguments's value.</value>
        public object Value { get; set; }
    }
}
