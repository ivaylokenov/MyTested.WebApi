namespace MyWebApi.Builders
{
    using System.Web.Http;

    using Base;
    using Contracts;

    public class ResponseModelErrorTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorTestBuilder<TResponseModel>
    {
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }
    }
}
