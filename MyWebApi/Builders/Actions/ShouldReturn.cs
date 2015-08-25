namespace MyWebApi.Builders.Actions
{
    using System;
    using Contracts.Models;
    using Models;

    /// <summary>
    /// Class containing methods for testing return type.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected response type.</param>
        /// <returns>Response model test builder.</returns>
        public IModelErrorTestBuilder ShouldReturn(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
            return new ModelErrorTestBuilder(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model test builder.</returns>
        public IModelErrorTestBuilder<TResponseModel> ShouldReturn<TResponseModel>()
        {
            this.ValidateActionReturnType<TResponseModel>(true);
            return new ModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }

        private TReturnObject GetReturnObject<TReturnObject>()
            where TReturnObject : class 
        {
            this.ValidateActionReturnType<TReturnObject>(true);
            return this.ActionResult as TReturnObject;
        }
    }
}
