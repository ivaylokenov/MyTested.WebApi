namespace MyWebApi.Builders.Contracts.Actions
{
    using And;
    using Base;
    using Models;

    public interface IShouldHaveTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Model test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> ModelStateFor<TRequestModel>();

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ValidModelState();

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> InvalidModelState();
    }
}
