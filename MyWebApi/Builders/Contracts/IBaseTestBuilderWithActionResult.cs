namespace MyWebApi.Builders.Contracts
{
    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<out TActionResult> : IBaseTestBuilder
    {
        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        TActionResult ActionResult { get; }
    }
}
