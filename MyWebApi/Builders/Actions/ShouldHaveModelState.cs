namespace MyWebApi.Builders.Actions
{
    using Contracts;
    using Exceptions;
    using ResponseModels;
    using Utilities;

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
        public IResponseModelErrorTestBuilder<TRequestModel> ShouldHaveModelStateFor<TRequestModel>()
        {
            return new ResponseModelErrorTestBuilder<TRequestModel>(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        public void ShouldHaveValidModelState()
        {
            this.CheckValidModelState();
        }

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        public void ShouldHaveInvalidModelState()
        {
            if (this.Controller.ModelState.Count == 0)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state, but was in fact valid.",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyGenericTypeName()));
            }
        }
    }
}
