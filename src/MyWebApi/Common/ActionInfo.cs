namespace MyWebApi.Common
{
    using System;

    internal class ActionInfo<TActionResult>
    {
        internal ActionInfo(string actionName, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        internal string ActionName { get; private set; }

        internal TActionResult ActionResult { get; private set; }

        internal Exception CaughtException { get; set; }
    }
}
