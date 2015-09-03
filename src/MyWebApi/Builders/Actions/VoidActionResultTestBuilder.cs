namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using Contracts.Base;
    using Utilities;

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
        /// <param name="caughtException">Caught exception during the action execution.</param>
        public VoidActionResultTestBuilder(ApiController controller, string actionName, Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }

        /// <summary>
        /// Tests whether action result is void.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilder ShouldReturnEmpty()
        {
            Validator.CheckForException(this.CaughtException);
            return this.NewAndProvideTestBuilder();
        }
    }
}
