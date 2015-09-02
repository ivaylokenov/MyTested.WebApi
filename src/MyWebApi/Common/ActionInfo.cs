namespace MyWebApi.Common
{
    internal class ActionInfo<TActionResult>
    {
        internal ActionInfo(string actionName, TActionResult actionResult)
        {
            this.ActionName = actionName;
            this.ActionResult = actionResult;
        }

        internal string ActionName { get; private set; }

        internal TActionResult ActionResult { get; private set; }
    }
}
