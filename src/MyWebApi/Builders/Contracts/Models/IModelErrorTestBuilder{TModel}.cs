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
    using System.Linq.Expressions;
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        IAndModelErrorTestBuilder<TModel> ContainingNoModelStateErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithNoError);
    }
}
