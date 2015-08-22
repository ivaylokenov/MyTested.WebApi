namespace MyWebApi.Builders
{
    using System.Web.Http;

    using Base;
    using Contracts;
    using Exceptions;

    public class ResponseModelErrorTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorTestBuilder<TResponseModel>
    {
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        public void ContainingNoErrors()
        {
            if (!this.Controller.ModelState.IsValid)
            {
                throw new ResponseModelErrorAssertionException("TODO");
            }
        }
    }
}
