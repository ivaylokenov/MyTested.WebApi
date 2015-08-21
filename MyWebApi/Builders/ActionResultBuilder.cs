namespace MyWebApi.Builders
{
    using Contracts;

    public class ActionResultBuilder<TActionResult> : IActionResultBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

        public ActionResultBuilder(string actionName, TActionResult actionResult)
        {
            this.actionName = actionName;
            this.actionResult = actionResult;
        }
    }
}
