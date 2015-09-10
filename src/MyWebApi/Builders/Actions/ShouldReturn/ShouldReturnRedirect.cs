namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;

    /// <summary>
    /// Class containing methods for testing RedirectResult or RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public void Redirect()
        {
            var actionResultAsRedirectResult = this.ActionResult as RedirectResult;
            if (actionResultAsRedirectResult != null)
            {
                
            }


        }
    }
}
