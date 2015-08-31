namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;
    using Contracts.InternalServerErrors;
    using InternalServerErrors;

    /// <summary>
    /// Class containing methods for testing InternalServerErrorResult or ExceptionResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is InternalServerErrorResult or ExceptionResult.
        /// </summary>
        /// <returns>Internal server error test builder.</returns>
        public IInternalServerErrorTestBuilder ShouldReturnInternalServerError()
        {
            if (this.ActionResult as InternalServerErrorResult != null)
            {
                return this.ReturnInternalServerErrorTestBuilder<InternalServerErrorResult>();
            }

            return this.ReturnInternalServerErrorTestBuilder<ExceptionResult>();
        }

        private IInternalServerErrorTestBuilder ReturnInternalServerErrorTestBuilder<TInternalServerErrorResult>()
            where TInternalServerErrorResult : class
        {
            var internalServerErrorResult = this.GetReturnObject<TInternalServerErrorResult>();
            return new InternalServerErrorTestBuilder<TInternalServerErrorResult>(
                this.Controller,
                this.ActionName,
                internalServerErrorResult);
        }
    }
}
