namespace MyWebApi.Builders.HttpResults
{
    using Contracts;

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
    }
}
