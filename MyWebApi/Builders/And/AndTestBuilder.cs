namespace MyWebApi.Builders.And
{
    using System.Web.Http;

    using Base;
    using Contracts.And;

    public class AndTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>,
        IAndTestBuilder<TActionResult>
    {
        public AndTestBuilder(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        public void And()
        {
            throw new System.NotImplementedException();
        }
    }
}
