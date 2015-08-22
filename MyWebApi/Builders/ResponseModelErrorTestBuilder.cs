namespace MyWebApi.Builders
{
    using System.Web.Http;
    using Contracts;

    public class ResponseModelErrorTestBuilder<TResponseModel> : IResponseModelErrorTestBuilder<TResponseModel>
    {
        public ResponseModelErrorTestBuilder(ApiController controller)
        {
        }
    }
}
