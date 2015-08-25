namespace MyWebApi.Builders.Actions
{
    using System.Web.Http.Results;

    using Contracts;
    using UnauthorizedResults;

    public partial class ActionResultTestBuilder<TActionResult>
    {
        public IUnauthorizedResultTestBuilder ShouldReturnUnauthorized()
        {
            var unathorizedResult = this.GetReturnObject<UnauthorizedResult>();
            return new UnauthorizedResultTestBuilder(this.Controller, this.ActionName, unathorizedResult);
        }
    }
}
