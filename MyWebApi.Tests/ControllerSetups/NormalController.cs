namespace MyWebApi.Tests.ControllerSetups
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

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

        public async Task<OkResult> AsyncEmptyAction()
        {
            return await Task.Run(() => this.Ok());
        }
    }
}
