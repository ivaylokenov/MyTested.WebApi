namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;
    using And;
    using Contracts.And;

    /// <summary>
    /// Class containing methods for testing NotFoundResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        public IAndTestBuilder<TActionResult> ShouldReturnNotFound()
        {
            this.ShouldReturn<NotFoundResult>();
            return this.NewAndTestBuilder();
        }
    }
}
