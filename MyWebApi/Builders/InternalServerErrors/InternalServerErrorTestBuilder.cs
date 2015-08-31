namespace MyWebApi.Builders.InternalServerErrors
{
    using System.Web.Http;
    using Base;
    using Contracts.InternalServerErrors;

    public class InternalServerErrorTestBuilder<TInternalServerErrorResult>
        : BaseTestBuilderWithActionResult<TInternalServerErrorResult>, IInternalServerErrorTestBuilder
    {
        public InternalServerErrorTestBuilder(
            ApiController controller,
            string actionName,
            TInternalServerErrorResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }
    }
}
