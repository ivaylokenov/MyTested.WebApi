namespace MyWebApi.Tests.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
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
            : this(injectedService, anotherInjectedService)
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

        public void EmptyAction()
        {
        }

        public async Task EmptyActionAsync()
        {
            await Task.Run(() => { });
        }

        public IHttpActionResult OkResultAction()
        {
            return this.Ok();
        }

        public void EmptyActionWithException()
        {
            this.ThrowNewNullReferenceException();
        }

        public IHttpActionResult ActionWithException()
        {
            throw new NullReferenceException("Test exception message");
        }

        public IHttpActionResult ActionWithHttpResponseException()
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public async Task EmptyActionWithExceptionAsync()
        {
            await Task.Run(() => this.ThrowNewNullReferenceException());
        }

        public async Task<IHttpActionResult> ActionWithExceptionAsync()
        {
            return await Task.Run(() =>
            {
                this.ThrowNewNullReferenceException();
                return this.Ok();
            });
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

        public IHttpActionResult OkResultWithInterfaceResponse()
        {
            return this.Ok(this.responseModel);
        }

        public IHttpActionResult OkResultWithResponse()
        {
            return this.Ok(this.responseModel.ToList());
        }

        public async Task<OkResult> AsyncOkResultAction()
        {
            return await Task.Run(() => this.Ok());
        }

        public IHttpActionResult BadRequestAction()
        {
            return this.BadRequest();
        }

        public IHttpActionResult BadRequestWithErrorAction()
        {
            return this.BadRequest("Bad request");
        }

        public IHttpActionResult BadRequestWithModelState(RequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                return this.Ok();
            }

            return this.BadRequest(this.ModelState);
        }

        public IHttpActionResult JsonAction()
        {
            return this.Json(this.responseModel);
        }

        public IHttpActionResult ConflictAction()
        {
            return this.Conflict();
        }

        public IHttpActionResult StatusCodeAction()
        {
            return this.StatusCode(HttpStatusCode.Redirect);
        }

        public IHttpActionResult CustomModelStateError()
        {
            this.ModelState.AddModelError("Test", "Test error");
            return this.Ok(this.responseModel);
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

        public IHttpActionResult UnauthorizedAction()
        {
            return this.Unauthorized();
        }

        public IHttpActionResult UnauthorizedActionWithChallenges()
        {
            return this.Unauthorized(new[]
            {
                new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                new AuthenticationHeaderValue("Basic"),
                new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
            });
        }

        public IHttpActionResult InternalServerErrorAction()
        {
            return this.InternalServerError();
        }

        public IHttpActionResult InternalServerErrorWithExceptionAction()
        {
            try
            {
                this.ThrowNewNullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                return this.InternalServerError(ex);
            }

            return this.Ok();
        }

        public bool GenericStructAction()
        {
            return true;
        }

        public IResponseModel GenericInterfaceAction()
        {
            return this.responseModel.FirstOrDefault();
        }

        public ResponseModel GenericAction()
        {
            return this.responseModel.FirstOrDefault();
        }

        public ICollection<ResponseModel> GenericActionWithCollection()
        {
            return this.responseModel;
        }

        public List<ResponseModel> GenericActionWithListCollection()
        {
            return TestObjectFactory.GetListOfResponseModels();
        }

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }
    }
}
