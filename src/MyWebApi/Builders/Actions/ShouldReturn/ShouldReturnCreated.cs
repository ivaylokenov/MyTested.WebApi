namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Created;
    using Created;
    using Utilities;

    /// <summary>
    /// Class containing methods for testing CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public ICreatedTestBuilder Created()
        {
            var typeOfActionResult = typeof(TActionResult);
            if (typeOfActionResult != typeof(CreatedNegotiatedContentResult<>)
                || typeOfActionResult != typeof (CreatedAtRouteNegotiatedContentResult<>))
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                "CreatedNegotiatedContentResult<T> or CreatedAtRouteNegotiatedContentResult<T>",
                typeOfActionResult.ToFriendlyTypeName());
            }

            return new CreatedTestBuilder<TActionResult>(this.Controller,
                    this.ActionName,
                    this.CaughtException,
                    this.ActionResult);
        }
    }
}
