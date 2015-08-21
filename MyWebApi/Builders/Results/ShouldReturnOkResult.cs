namespace MyWebApi.Builders.Results
{
    using System.Web.Http.Results;
    using Contracts;

    /// <summary>
    /// Class containing methods for testing OkResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is plain OkResult.
        /// </summary>
        public IResponseModelTestBuilder<TActionResult> ShouldReturnOkResult()
        {
            this.ShouldReturn<OkResult>();
            return new ResponseModelTestBuilder<TActionResult>(this.ActionName, this.ActionResult);
        }
    }
}
