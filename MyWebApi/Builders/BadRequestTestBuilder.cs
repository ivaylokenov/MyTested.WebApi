namespace MyWebApi.Builders
{
    using System.Web.Http;
    using Base;
    using Contracts;

    public class BadRequestTestBuilder<TBadRequestResult> : BaseTestBuilderWithActionResult<TBadRequestResult>,
        IBadRequestTestBuilder
    {
        public BadRequestTestBuilder(
            ApiController controller,
            string actionName,
            TBadRequestResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }
    }
}
