namespace MyWebApi.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Contracts;

    public class ControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        private TController controller;

        public ControllerBuilder(TController controllerInstance)
        {
            this.controller = controllerInstance;
        }

        public void Calling<TAction>(Expression<Func<TController, TAction>> actionCall)
        {
            throw new NotImplementedException();
        }

        public void Calling<TAction>(Expression<Func<TController, Task<TAction>>> actionCall)
        {
            throw new NotImplementedException();
        }
    }
}
