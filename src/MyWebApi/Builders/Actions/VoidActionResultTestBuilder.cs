namespace MyWebApi.Builders.Actions
{
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using Contracts.Base;

    public class VoidActionResultTestBuilder : BaseTestBuilder, IVoidActionResultTestBuilder
    {
        public VoidActionResultTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        public IBaseTestBuilder ShouldReturnEmpty()
        {
            return this.NewAndProvideTestBuilder();
        }
    }
}
