namespace MyWebApi.Tests.ControllerSetups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    internal class WebApiController : ApiController
    {
        public WebApiController()
            : this(new InjectedService())
        {
        }

        public WebApiController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
        }

        public IInjectedService InjectedService { get; private set; }

        public IHttpActionResult OkResultAction()
        {
            return this.Ok();
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IHttpActionResult BadRequestAction()
        {
            return this.BadRequest();
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public ICollection<ResponseModel> GenericAction()
        {
            return new List<ResponseModel>
            {
                new ResponseModel { Id = 1, Name = "Test" },
                new ResponseModel { Id = 2, Name = "Another Test" }
            };
        }
    }
}
