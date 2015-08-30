namespace MyWebApi.Builders.And
{
    using System.Web.Http;
    using Base;
    using Contracts.And;

    public class AndTestBuilder : BaseTestBuilder, IAndTestBuilder
    {
        protected AndTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        public IAndContinuityTestBuilder And()
        {
            return new AndContinuityTestBuilder(this.Controller, this.ActionName);
        }
    }
}
