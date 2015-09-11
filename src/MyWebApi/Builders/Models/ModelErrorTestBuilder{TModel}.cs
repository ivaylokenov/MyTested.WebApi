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
    using System.Linq.Expressions;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Common.Extensions;
    using Contracts.Models;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ModelErrorTestBuilder<TModel> : ModelErrorTestBuilder, IAndModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorTestBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="model">Model returned from action result.</param>
        /// <param name="modelState">Optional model state dictionary to use the class with. Default is controller's model state.</param>
        public ModelErrorTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TModel model = default(TModel),
            ModelStateDictionary modelState = null)
            : base(controller, actionName, caughtException, modelState)
        {
            this.Model = model;
        }

        /// <summary>
        /// Gets model from invoked action in ASP.NET Web API controller.
        /// </summary>
        /// <value>Model from invoked action.</value>
        protected TModel Model { get; private set; }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingModelStateError(string errorKey)
        {
            if (!this.ModelState.ContainsKey(errorKey) || this.ModelState.Count == 0)
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    errorKey);
            }

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorDetailsTestBuilder<TModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithError);
            this.ContainingModelStateError(memberName);

            return new ModelErrorDetailsTestBuilder<TModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Model,
                this,
                memberName,
                this.ModelState[memberName].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        public IAndModelErrorTestBuilder<TModel> ContainingNoModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithNoError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithNoError);
            if (this.ModelState.ContainsKey(memberName))
            {
                this.ThrowNewModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have no model errors against key {2}, but found some.",
                    memberName);
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        public IModelErrorTestBuilder<TModel> AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        public TModel AndProvideTheModel()
        {
            Validator.CheckForEqualityWithDefaultValue(this.Model, "AndProvideTheModel can be used when there is response model from the action.");
            return this.Model;
        }

        private void ThrowNewModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetName(),
                    errorKey));
        }
    }
}
