namespace MyWebApi.Builders.Results
{
    using System.Web.Http.Results;

    /// <summary>
    /// Class containing methods for testing OkResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is plain OkResult.
        /// </summary>
        public void ShouldReturnOkResult()
        {
            this.ValidateActionReturnType<OkResult>();
        }
    }
}
