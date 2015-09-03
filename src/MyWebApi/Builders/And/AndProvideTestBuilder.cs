namespace MyWebApi.Builders.And
{
    using System;
    using System.Web.Http;
    using Base;

    /// <summary>
    /// Provides controller and action information.
    /// </summary>
    public class AndProvideTestBuilder : BaseTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndProvideTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        public AndProvideTestBuilder(ApiController controller, string actionName, Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }
    }
}
