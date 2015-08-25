namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;

    /// <summary>
    /// Class containing methods for testing BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        public void ShouldReturnBadRequest()
        {
            if (this.ActionResult as BadRequestResult != null)
            {
                this.ShouldReturn<BadRequestResult>();
            }
            else if (this.ActionResult as InvalidModelStateResult != null)
            {
                this.ShouldReturn<InvalidModelStateResult>();
            }
            else
            {
                this.ShouldReturn<BadRequestErrorMessageResult>();
            }
        }
    }
}
