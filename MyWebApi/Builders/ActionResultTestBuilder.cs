namespace MyWebApi.Builders
{
    using Contracts;

    public class ActionResultTestBuilder<TActionResult> : IActionResultTestBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

        public ActionResultTestBuilder(string actionName, TActionResult actionResult)
        {
            this.actionName = actionName;
            this.actionResult = actionResult;
        }
    }
}
