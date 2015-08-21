namespace MyWebApi.Builders
{
    using Contracts;

    public class ResponseModelTestBuilder<TActionResult>
        : BaseTestBuilder<TActionResult>, IResponseModelTestBuilder<TActionResult>
    {
        public ResponseModelTestBuilder(string actionName, TActionResult actionResult)
            : base(actionName, actionResult)
        {
        }
    }
}
