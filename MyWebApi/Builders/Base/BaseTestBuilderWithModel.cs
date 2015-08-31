﻿namespace MyWebApi.Builders.Base
{
    using System.Web.Http;
    using Contracts.Base;
    using Utilities;

    /// <summary>
    /// Base class for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public class BaseTestBuilderWithModel<TModel> : BaseTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithModel{TModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="model">Model returned from action result.</param>
        protected BaseTestBuilderWithModel(ApiController controller, string actionName, TModel model)
            : base(controller, actionName)
        {
            this.Model = model;
        }

        internal TModel Model { get; private set; }

        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        public TModel AndProvideTheModel()
        {
            Validator.CheckForEqualityWithDefaultValue(this.Model, "AndProvideTheModel can be used when there is response model from the action.");
            return this.Model;
        }
    }
}
