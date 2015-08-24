namespace MyWebApi.Tests.Setups
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Models;
    using Services;

    internal class WebApiController : ApiController
    {
        private readonly ICollection<ResponseModel> responseModel;

        public WebApiController()
            : this(new InjectedService())
        {
        }

        public WebApiController(IInjectedService injectedService)
        {
            this.InjectedService = injectedService;
            this.responseModel = TestObjectFactory.GetListOfResponseModels();
        }

        public WebApiController(RequestModel requestModel)
        {
            this.InjectedRequestModel = requestModel;
        }

        public WebApiController(IInjectedService injectedService, RequestModel requestModel) 
            : this(injectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public WebApiController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService)
            : this(injectedService)
        {
            this.AnotherInjectedService = anotherInjectedService;
        }

        public WebApiController(IInjectedService injectedService, IAnotherInjectedService anotherInjectedService, RequestModel requestModel)
            :this(injectedService, anotherInjectedService)
        {
            this.InjectedRequestModel = requestModel;
        }

        public ICollection<ResponseModel> ResponseModel
        {
            get { return this.responseModel; }
        }

        public IInjectedService InjectedService { get; private set; }

        public IAnotherInjectedService AnotherInjectedService { get; private set; }

        public RequestModel InjectedRequestModel { get; private set; }

        public IHttpActionResult OkResultAction()
        {
            return this.Ok();
        }

        public IHttpActionResult OkResultActionWithRequestBody(int id, RequestModel model)
        {
            return this.Ok(this.responseModel);
        }

        public IHttpActionResult ModelStateCheck(RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return this.Ok(model);
            }

            return this.Ok(model);
        }

        public IHttpActionResult OkResultWithResponse()
        {
            return this.Ok(this.responseModel);
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IHttpActionResult BadRequestAction()
        {
            return this.BadRequest();
        }

        public IHttpActionResult StatusCodeAction()
        {
            return this.StatusCode(HttpStatusCode.Redirect);
        }

        public IHttpActionResult NotFoundAction()
        {
            return this.NotFound();
        }

        [Authorize]
        public IHttpActionResult AuthorizedAction()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public ICollection<ResponseModel> GenericAction()
        {
            return this.responseModel;
        }
    }
}
