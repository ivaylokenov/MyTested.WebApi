namespace MyWebApi.Builders.And
{
    using System.Web.Http;
    using Base;
    using Contracts.And;

    public class AndContinuityTestBuilder : BaseTestBuilder, IAndContinuityTestBuilder
    {
        public AndContinuityTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        public ApiController ProvideTheControllerInstance()
        {
            return this.Controller;
        }
    }
}
