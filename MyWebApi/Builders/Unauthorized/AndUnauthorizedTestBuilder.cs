namespace MyWebApi.Builders.Unauthorized
{
    using System.Web.Http;
    using System.Web.Http.Results;

    using Contracts.UnauthorizedResults;

    /// <summary>
    /// Used for adding And() method to the the unauthorized response tests.
    /// </summary>
    public class AndUnauthorizedTestBuilder : UnauthorizedTestBuilder, IAndUnauthorizedTestBuilder
    {
        private readonly IUnauthorizedTestBuilder unauthorizedResultTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AndUnauthorizedTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        /// <param name="unauthorizedResultTestBuilder">The original unauthorized result test builder.</param>
        public AndUnauthorizedTestBuilder(
            ApiController controller,
            string actionName,
            UnauthorizedResult actionResult,
            IUnauthorizedTestBuilder unauthorizedResultTestBuilder)
            : base(controller, actionName, actionResult)
        {
            this.unauthorizedResultTestBuilder = unauthorizedResultTestBuilder;
        }

        /// <summary>
        /// And method for better readability when chaining unauthorized result tests.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        public IUnauthorizedTestBuilder And()
        {
            return this.unauthorizedResultTestBuilder;
        }
    }
}
