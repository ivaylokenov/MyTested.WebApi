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

namespace MyWebApi.Builders.Contracts.HttpActionResults.Redirect
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;
    using Base;
    using Uris;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    public interface IRedirectTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(string location);

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(Uri location);

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        IBaseTestBuilderWithCaughtException To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        IBaseTestBuilderWithCaughtException To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;
    }
}
