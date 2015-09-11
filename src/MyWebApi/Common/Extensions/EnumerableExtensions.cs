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

namespace MyWebApi.Common.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides extension methods to IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Provides easier linear traversing over all items in collection and executing a function on each of them.
        /// </summary>
        /// <typeparam name="T">Type of objects in the collection.</typeparam>
        /// <param name="collection">Collection to traverse.</param>
        /// <param name="action">Function to execute on each item in the collection.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
