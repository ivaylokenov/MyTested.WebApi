namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Redirect;
    using Redirect;

    /// <summary>
    /// Class containing methods for testing RedirectResult or RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public IRedirectTestBuilder Redirect()
        {
            var actionResultAsRedirectResult = this.ActionResult as RedirectToRouteResult;
            if (actionResultAsRedirectResult != null)
            {
                return this.ReturnRedirectTestBuilder<RedirectToRouteResult>();
            }

            return this.ReturnRedirectTestBuilder<RedirectResult>();
        }

        private IRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
            where TRedirectResult : class
        {
            var redirectResult = this.GetReturnObject<TRedirectResult>();
            return new RedirectTestBuilder<TRedirectResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                redirectResult);
        }
    }
}
