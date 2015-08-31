namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;
    using Contracts.Base;

    /// <summary>
    /// Class containing methods for testing ConflictResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ConflictResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> ShouldReturnConflict()
        {
            this.ShouldReturn<ConflictResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
