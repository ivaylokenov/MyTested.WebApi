namespace MyWebApi.Builders.Base
{
    using System.Web.Http;
    using Contracts.Base;
    using Utilities;

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
            Validator.CheckForEqualityWithDefaultValue(this.Model, "AndProvideTheModel can be used when there is response model from the action.");
            return this.Model;
        }
    }
}
