namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;

    using BadRequests;

    using Contracts.BadRequests;

    /// <summary>
    /// Class containing methods for testing BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        public IBadRequestTestBuilder ShouldReturnBadRequest()
        {
            if (this.ActionResult as BadRequestResult != null)
            {
                return this.ReturnBadRequestTestBuilder<BadRequestResult>();
            }

            if (this.ActionResult as InvalidModelStateResult != null)
            {
                return this.ReturnBadRequestTestBuilder<InvalidModelStateResult>();
            }

            return this.ReturnBadRequestTestBuilder<BadRequestErrorMessageResult>();
        }

        private IBadRequestTestBuilder ReturnBadRequestTestBuilder<TBadRequestResult>()
            where TBadRequestResult : class
        {
            var badRequestResult = this.GetReturnObject<TBadRequestResult>();
            return new BadRequestTestBuilder<TBadRequestResult>(this.Controller, this.ActionName, badRequestResult);
        }
    }
}
