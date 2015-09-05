namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Models;
    using Models;

    /// <summary>
    /// Class containing methods for testing OkResult and OkNegotiatedContentResult{T}.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is plain OkResult.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        public IResponseModelTestBuilder Ok()
        {
            var actionResultAsOkResult = this.ActionResult as OkResult;
            if (actionResultAsOkResult != null)
            {
                this.ResultOfType<OkResult>();
            }
            else
            {
                this.ValidateActionReturnType(typeof(OkNegotiatedContentResult<>), allowDifferentGenericTypeDefinitions: true);
            }

            return new ResponseModelTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
