namespace MyWebApi.Builders.Contracts.Actions
{
    using And;
    using Base;
    using Models;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Model test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> ShouldHaveModelStateFor<TRequestModel>();

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ShouldHaveValidModelState();

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ShouldHaveInvalidModelState();

        IShouldThrowTestBuilder ShouldThrow();

        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}
