namespace MyWebApi.Tests.ControllerSetups
{
    using System.Web.Http;

    internal class NormalController : ApiController
    {
        public NormalController()
            : this(new InjectedService())
        {
        }

        public NormalController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
        }

        public IInjectedService InjectedService { get; private set; }

        public IHttpActionResult EmptyAction()
        {
            return this.Ok();
        }
    }
}
