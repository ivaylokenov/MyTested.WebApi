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

        public IActionResultBuilder<TAction> Calling<TAction>(Expression<Func<TController, TAction>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.controller);
            return new ActionResultBuilder<TAction>(actionName, actionResult);
        }

        public IActionResultBuilder<TAction> Calling<TAction>(Expression<Func<TController, Task<TAction>>> actionCall)
        {
            var actionName = ExpressionParser.GetMethodName(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.controller).Result;
            return new ActionResultBuilder<TAction>(actionName, actionResult);
        }
    }
}
