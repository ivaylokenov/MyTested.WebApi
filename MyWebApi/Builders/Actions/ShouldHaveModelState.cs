namespace MyWebApi.Builders.Actions
{
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Models;
    using Exceptions;
    using Models;

    /// <summary>
    /// Class containing methods for testing return type.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Response model test builder.</returns>
        public IModelErrorTestBuilder<TRequestModel> ShouldHaveModelStateFor<TRequestModel>()
        {
            return new ModelErrorTestBuilder<TRequestModel>(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        public IAndTestBuilder<TActionResult> ShouldHaveValidModelState()
        {
            this.CheckValidModelState();
            return this.NewAndTestBuilder();
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        public IAndTestBuilder<TActionResult> ShouldHaveInvalidModelState()
        {
            if (this.Controller.ModelState.Count == 0)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state, but was in fact valid.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return this.NewAndTestBuilder();
        }
    }
}
