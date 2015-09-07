namespace MyWebApi.Tests.Setups.Controllers
{
    using System.Web.Http;
    using Services;

    public class NoParameterlessConstructorController : ApiController
    {
        private IInjectedService service;

        internal NoParameterlessConstructorController(IInjectedService service)
        {
            this.service = service;
        }

        public IHttpActionResult OkAction()
        {
            return this.Ok();
        }
    }
}
