namespace MyWebApi.Builders.UnauthorizedResults
{
    using System.Web.Http;
    using System.Web.Http.Results;

    using Contracts;

    public class AndUnauthorizedTestBuilder : UnauthorizedResultTestBuilder, IAndUnauthorizedResultTestBuilder
    {
        private readonly IUnauthorizedResultTestBuilder unauthorizedResultTestBuilder;

        public AndUnauthorizedTestBuilder(
            ApiController controller,
            string actionName,
            UnauthorizedResult actionResult,
            IUnauthorizedResultTestBuilder unauthorizedResultTestBuilder)
            : base(controller, actionName, actionResult)
        {
            this.unauthorizedResultTestBuilder = unauthorizedResultTestBuilder;
        }

        public IUnauthorizedResultTestBuilder And()
        {
            return this.unauthorizedResultTestBuilder;
        }
    }
}
