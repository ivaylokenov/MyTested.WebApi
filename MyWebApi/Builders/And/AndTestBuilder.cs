namespace MyWebApi.Builders.And
{
    using System.Web.Http;
    using Actions;
    using Base;
    using Contracts.Actions;
    using Contracts.And;

    public class AndTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>,
        IAndTestBuilder<TActionResult>
    {
        public AndTestBuilder(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        public IActionResultTestBuilder<TActionResult> And()
        {
            return new ActionResultTestBuilder<TActionResult>(this.Controller, this.ActionName, this.ActionResult);
        }
    }
}
