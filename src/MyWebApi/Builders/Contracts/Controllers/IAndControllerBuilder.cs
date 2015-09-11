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

namespace MyWebApi.Builders.Contracts.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// Used for adding AndAlso() method to controller builder.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public interface IAndControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> AndAlso();
    }
}
