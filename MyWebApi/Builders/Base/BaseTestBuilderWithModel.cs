namespace MyWebApi.Builders.Base
{
    using System.Web.Http;
    using Contracts.Base;

    public class BaseTestBuilderWithModel<TModel> : BaseTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        protected BaseTestBuilderWithModel(ApiController controller, string actionName, TModel model)
            : base(controller, actionName)
        {
            this.Model = model;
        }

        internal TModel Model { get; private set; }

        public TModel AndProvideTheModel()
        {
            return this.Model;
        }
    }
}
