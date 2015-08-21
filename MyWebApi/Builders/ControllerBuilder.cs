namespace MyWebApi.Builders
{
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

        public void Calling<TAction>()
        {
            throw new System.NotImplementedException();
        }
    }
}
