namespace MyWebApi.Builders.InternalServerErrors
{
    using System.Web.Http;
    using Base;
    using Contracts.InternalServerErrors;

    /// <summary>
    /// Used for testing internal server error results.
    /// </summary>
    /// <typeparam name="TInternalServerErrorResult">Type of internal server error result - InternalServerErrorResult or ExceptionResult.</typeparam>
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
