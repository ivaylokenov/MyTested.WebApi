namespace MyWebApi.Builders.And
{
    using System.Web.Http;
    using Base;

    public class AndProvideTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>
    {
        public AndProvideTestBuilder(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }
    }
}
