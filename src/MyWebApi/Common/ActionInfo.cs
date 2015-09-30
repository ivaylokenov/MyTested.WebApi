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
    using System.Collections.Generic;

    internal class ActionInfo<TActionResult>
    {
        internal ActionInfo(string actionName, IEnumerable<object> actionAttributes, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.ActionAttributes = actionAttributes;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        internal string ActionName { get; private set; }

        internal IEnumerable<object> ActionAttributes { get; private set; } 

        internal TActionResult ActionResult { get; private set; }

        internal Exception CaughtException { get; set; }
    }
}
