namespace MyWebApi.Builders.Actions
{
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using Contracts.Base;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public class VoidActionResultTestBuilder : BaseTestBuilder, IVoidActionResultTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidActionResultTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public VoidActionResultTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        /// <summary>
        /// Tests whether action result is void.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder ShouldReturnEmpty()
        {
            return this.NewAndProvideTestBuilder();
        }
    }
}
