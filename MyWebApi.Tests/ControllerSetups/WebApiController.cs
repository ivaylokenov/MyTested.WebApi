namespace MyWebApi.Tests.ControllerSetups
{
    using System.Collections.Generic;
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
            InjectedService = injectedService;
            responseModel = TestObjectFactory.GetListOfResponseModels();
        }

        public ICollection<ResponseModel> ResponseModel
        {
            get { return responseModel; }
        }

        public IInjectedService InjectedService { get; private set; }

        public IHttpActionResult OkResultAction()
        {
            return Ok();
        }

        public IHttpActionResult OkResultActionWithRequestBody(int id, RequestModel model)
        {
            return Ok(responseModel);
        }

        public IHttpActionResult ModelStateCheck(RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }

            return Ok(model);
        }

        public IHttpActionResult OkResultWithResponse()
        {
            return Ok(responseModel);
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => Ok());
        }

        public IHttpActionResult BadRequestAction()
        {
            return BadRequest();
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public ICollection<ResponseModel> GenericAction()
        {
            return responseModel;
        }
    }
}
