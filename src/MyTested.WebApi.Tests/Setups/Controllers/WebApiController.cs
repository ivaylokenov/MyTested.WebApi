// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Models;
    using Newtonsoft.Json;
    using Services;

    [Authorize(Roles = "Admin,Moderator", Users = "John,George")]
    [RoutePrefix("api/test")]
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

        public IHttpActionResult WithGeneratedLink(int id)
        {
            var link = this.Url.Link("TestRouteAttributes", new { id = 1 });
            return this.Created(link, "content");
        }

        public IHttpActionResult CustomRequestAction()
        {
            if (this.Request.Method == HttpMethod.Post && this.Request.Headers.Contains("TestHeader"))
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public IHttpActionResult CommonHeaderAction()
        {
            if (this.Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue(MediaType.ApplicationJson)))
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        public HttpResponseMessage HttpResponseMessageAction()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                ReasonPhrase = "Custom reason phrase",
                Version = new Version(1, 1),
                Content = new ObjectContent(this.responseModel.GetType(), this.responseModel, TestObjectFactory.GetCustomMediaTypeFormatter()),
                RequestMessage = this.Request
            };

            response.Headers.Add("TestHeader", "TestHeaderValue");
            response.Content.Headers.Add("TestHeader", "TestHeaderValue");

            return response;
        }

        public HttpResponseMessage HttpResponseMessageGenericObjectContentAction()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<IEnumerable<ResponseModel>>(this.responseModel, new JsonMediaTypeFormatter())
            };
        }

        public HttpResponseMessage HttpResponseMessageWithStringContent()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("Test string")
            };
        }

        public HttpResponseMessage HttpResponseMessageWithResponseModelAction()
        {
            return this.Request.CreateResponse(HttpStatusCode.BadRequest, this.responseModel);
        }

        public HttpResponseMessage HttpResponseMessageWithMediaTypeFormatter()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                this.responseModel,
                TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        public HttpResponseMessage HttpResponseMessageWithoutContent()
        {
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage HttpResponseMessageWithMediaType()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                this.responseModel,
                MediaType.ApplicationJson);
        }

        public HttpResponseMessage HttpResponseMessageWithFormatterAndMediaType()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                this.responseModel, 
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                MediaType.ApplicationJson);
        }

        public HttpResponseMessage HttpResponseError()
        {
            return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new InvalidOperationException("Error"));
        }

        public HttpResponseMessage HttpResponseErrorWithHttpError()
        {
            return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new HttpError("Error"));
        }

        public HttpResponseMessage HttpResponseErrorWithModelState()
        {
            return this.Request.CreateErrorResponse(HttpStatusCode.OK, this.ModelState);
        }

        public HttpResponseMessage HttpResponseErrorWithStringMessage()
        {
            return this.Request.CreateErrorResponse(HttpStatusCode.OK, "Error");
        }

        public void EmptyAction()
        {
        }

        public void EmptyActionWithParameters(int id, RequestModel model)
        {
        }

        public async Task EmptyActionAsync()
        {
            await Task.Run(() => { });
        }

        [Authorize]
        [HttpGet]
        public void EmptyActionWithAttributes()
        {
        }

        [Authorize(Roles = "Admin,Moderator", Users = "John,George")]
        [HttpGet]
        [HttpHead]
        public IHttpActionResult NormalActionWithAttributes()
        {
            return this.Ok();
        }

        [AllowAnonymous]
        [Route("/api/test", Name = "TestRoute", Order = 1)]
        [ActionName("NormalAction")]
        [NonAction]
        [AcceptVerbs("Get", "Post")]
        [HttpDelete]
        public IHttpActionResult VariousAttributesAction()
        {
            return this.Ok();
        }

        public IHttpActionResult OkResultAction()
        {
            return this.Ok();
        }

        public IHttpActionResult OkResultWithContentNegotiatorAction()
        {
            return new OkNegotiatedContentResult<int>(
                5,
                TestObjectFactory.GetCustomContentNegotiator(),
                TestObjectFactory.GetCustomHttpRequestMessage(),
                TestObjectFactory.GetFormatters());
        }

        public void EmptyActionWithException()
        {
            this.ThrowNewNullReferenceException();
        }

        public IHttpActionResult NullAction()
        {
            return null;
        }

        public int DefaultStructAction()
        {
            return 0;
        }

        public IHttpActionResult ContentAction()
        {
            return this.Content(HttpStatusCode.OK, this.responseModel);
        }

        public IHttpActionResult ContentActionWithMediaType()
        {
            return this.Content(
                HttpStatusCode.OK,
                this.responseModel,
                TestObjectFactory.GetCustomMediaTypeFormatter(),
                TestObjectFactory.MediaType);
        }

        public IHttpActionResult ContentActionWithNullMediaType()
        {
            return this.Content(
                HttpStatusCode.OK,
                this.responseModel,
                TestObjectFactory.GetCustomMediaTypeFormatter());
        }

        public IHttpActionResult ContentActionWithCustomFormatters()
        {
            return new NegotiatedContentResult<int>(
                HttpStatusCode.OK,
                5,
                TestObjectFactory.GetCustomContentNegotiator(),
                TestObjectFactory.GetCustomHttpRequestMessage(),
                TestObjectFactory.GetFormatters());
        }

        public IHttpActionResult CreatedAction()
        {
            return this.Created(TestObjectFactory.GetUri().OriginalString, this.responseModel);
        }

        public IHttpActionResult CreatedActionWithCustomContentNegotiator()
        {
            return new CreatedNegotiatedContentResult<ICollection<ResponseModel>>(
                TestObjectFactory.GetUri(),
                this.responseModel,
                TestObjectFactory.GetCustomContentNegotiator(),
                TestObjectFactory.GetCustomHttpRequestMessage(),
                TestObjectFactory.GetFormatters());
        }

        public IHttpActionResult CreatedActionWithUri()
        {
            return this.Created(TestObjectFactory.GetUri(), this.responseModel);
        }

        public IHttpActionResult CreatedAtRouteAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "WithParameter", id = 1 }, this.responseModel);
        }

        public IHttpActionResult CreatedAtRouteVoidAction()
        {
            return this.CreatedAtRoute("Redirect", new { action = "VoidAction" }, this.responseModel);
        }

        public IHttpActionResult RedirectAction()
        {
            return this.Redirect(TestObjectFactory.GetUri().OriginalString);
        }

        public IHttpActionResult RedirectActionWithUri()
        {
            return this.Redirect(TestObjectFactory.GetUri());
        }

        public IHttpActionResult RedirectToRouteAction()
        {
            return this.RedirectToRoute("Redirect", new { action = "WithParameter", id = 1 });
        }

        public IHttpActionResult RedirectToRouteVoidAction()
        {
            return this.RedirectToRoute("Redirect", new { action = "VoidAction" });
        }

        public IHttpActionResult ActionWithException()
        {
            throw new NullReferenceException("Test exception message");
        }

        public IHttpActionResult ActionWithAggregateException()
        {
            throw new AggregateException(new NullReferenceException(), new InvalidOperationException());
        }

        public IHttpActionResult ActionWithHttpResponseException()
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IHttpActionResult ActionWithHttpResponseExceptionAndHttpResponseMessageException()
        {
            throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.InternalServerError));
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

        public IHttpActionResult OkResultWithParameter(int id)
        {
            return this.Ok(id);
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

        public IHttpActionResult JsonWithEncodingAction()
        {
            return this.Json(this.responseModel, new JsonSerializerSettings(), Encoding.ASCII);
        }

        public IHttpActionResult JsonWithSettingsAction()
        {
            return this.Json(this.responseModel, TestObjectFactory.GetJsonSerializerSettings());
        }

        public IHttpActionResult JsonWithSpecificSettingsAction(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.Json(this.responseModel, jsonSerializerSettings);
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
                new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter")
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

        public dynamic DynamicResult()
        {
            return TestObjectFactory.GetListOfResponseModels();
        }

        private void ThrowNewNullReferenceException()
        {
            throw new NullReferenceException("Test exception message");
        }
    }
}
