namespace MyWebApi.Builders
{
    using Contracts;

    /// <summary>
    /// Used for specifying the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class ActionResultTestBuilder<TActionResult> : IActionResultTestBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(string actionName, TActionResult actionResult)
        {
            this.actionName = actionName;
            this.actionResult = actionResult;
        }
    }
}
