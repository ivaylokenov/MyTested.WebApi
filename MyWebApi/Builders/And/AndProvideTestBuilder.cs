namespace MyWebApi.Builders.And
{
    using System.Web.Http;
    using Base;

    public class AndProvideTestBuilder : BaseTestBuilder
    {
        public AndProvideTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }
    }
}
