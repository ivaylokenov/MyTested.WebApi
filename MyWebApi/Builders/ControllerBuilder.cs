namespace MyWebApi.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Contracts;
    using Utilities;

    /// <summary>
    /// Used for building the action which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public class ControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}" /> class.
        /// </summary>
        /// <param name="controllerInstance">Instance of the tested ASP.NET Web API controller.</param>
        public ControllerBuilder(TController controllerInstance)
        {
            this.Controller = controllerInstance;
        }

        /// <summary>
        /// Gets ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET Web API controller.</value>
        public TController Controller { get; private set; }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.Controller);
            return new ActionResultTestBuilder<TActionResult>(actionName, actionResult);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.Controller).Result;
            return new ActionResultTestBuilder<TActionResult>(actionName, actionResult);
        }
    }
}
