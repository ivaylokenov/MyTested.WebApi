namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Base;

    /// <summary>
    /// Class containing methods for testing NotFoundResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> NotFound()
        {
            this.ResultOfType<NotFoundResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
