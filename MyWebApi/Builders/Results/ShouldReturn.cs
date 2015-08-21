namespace MyWebApi.Builders.Results
{
    using System;

    /// <summary>
    /// Class containing methods for testing return type.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        public void ShouldReturn(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
        }

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseData">Expected response type.</typeparam>
        public void ShouldReturn<TResponseData>()
        {
            this.ValidateActionReturnType<TResponseData>(true);
        }
    }
}
