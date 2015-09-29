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

namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;

    public class ActionAttributeTestBuilder<TActionResult> 
        : BaseTestBuilderWithActionResult<TActionResult>, IActionAttributeTestBuilder<TActionResult>
    {
        public ActionAttributeTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionResult, actionAttributes)
        {
        }
    }
}
