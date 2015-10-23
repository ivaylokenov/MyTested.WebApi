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

namespace MyWebApi.Tests.Setups.Models
{
    using System;

    public class ComparableModel : IComparable
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public int CompareTo(object obj)
        {
            var objAsComparableModel = (ComparableModel)obj;
            if (this.Integer < objAsComparableModel.Integer)
            {
                return -1;
            }
            else if (this.Integer > objAsComparableModel.Integer)
            {
                return 1;
            }

            return 0;
        }
    }
}
