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

namespace MyWebApi.Builders.Models
{
    using System;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Models;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ModelDetailsTestBuilder<TResponseModel>
        : ModelErrorTestBuilder<TResponseModel>, IModelDetailsTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="responseModel">Response model from invoked action.</param>
        public ModelDetailsTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TResponseModel responseModel)
            : base(controller, actionName, caughtException, responseModel)
        {
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions)
        {
            assertions(this.Model);
            return new ModelErrorTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.Model))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ModelErrorTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model);
        }
    }
}
