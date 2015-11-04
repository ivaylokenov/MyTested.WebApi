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

namespace MyWebApi.Builders.Contracts.Models
{
    using System;
    using Base;
    using HttpResponseMessages;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from HTTP response message content.</typeparam>
    public interface IHttpHandlerModelDetailsTestBuilder<out TResponseModel> : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Tests whether the returned response model from the tested handler passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the HTTP response message results from handlers.</returns>
        IHttpHandlerResponseMessageTestBuilder Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the tested handler passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the HTTP response message results from handlers.</returns>
        IHttpHandlerResponseMessageTestBuilder Passing(Func<TResponseModel, bool> predicate);

        /// <summary>
        /// Gets the HTTP response message content model used in the testing.
        /// </summary>
        /// <returns>Instance of the content model type.</returns>
        TResponseModel AndProvideTheModel();
    }
}
