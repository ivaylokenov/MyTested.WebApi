namespace MyWebApi.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Contracts;
    using Utilities;

    public class ControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        private readonly TController controller;

        public ControllerBuilder(TController controllerInstance)
        {
            this.controller = controllerInstance;
        }

        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.controller);
            return new ActionResultTestBuilder<TActionResult>(actionName, actionResult);
        }

        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.controller).Result;
            return new ActionResultTestBuilder<TActionResult>(actionName, actionResult);
        }
    }
}
