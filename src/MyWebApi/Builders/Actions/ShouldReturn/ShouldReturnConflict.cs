namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Base;

    /// <summary>
    /// Class containing methods for testing ConflictResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ConflictResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Conflict()
        {
            this.ResultOfType<ConflictResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
