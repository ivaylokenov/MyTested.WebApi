namespace MyWebApi.Builders.Results
{
    using System;

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
        public void ShouldReturn(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
        }

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        public void ShouldReturn<TResponseModel>()
        {
            this.ValidateActionReturnType<TResponseModel>(true);
        }
    }
}
