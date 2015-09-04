namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Web.Http.Results;
    using Contracts.Json;
    using Json;

    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public IJsonTestBuilder Json()
        {
            this.ResultOfType(typeof(JsonResult<>));
            return new JsonTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
