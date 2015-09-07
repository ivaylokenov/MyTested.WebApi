namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Unauthorized;
    using Unauthorized;

    /// <summary>
    /// Class containing methods for testing UnauthorizedResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        public IUnauthorizedTestBuilder Unauthorized()
        {
            var unathorizedResult = this.GetReturnObject<UnauthorizedResult>();
            return new UnauthorizedTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                unathorizedResult);
        }
    }
}
