namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;

    /// <summary>
    /// Class containing methods for testing NotFoundResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        public void ShouldReturnNotFound()
        {
            this.ShouldReturn<NotFoundResult>();
        }
    }
}
