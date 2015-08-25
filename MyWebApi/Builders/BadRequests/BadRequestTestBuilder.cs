namespace MyWebApi.Builders.BadRequests
{
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;

    using Base;
    using Contracts;
    using Exceptions;
    using Utilities;

    public class BadRequestTestBuilder<TBadRequestResult> : BaseTestBuilderWithActionResult<TBadRequestResult>,
        IBadRequestTestBuilder
    {
        private const string ErrorMessage = "error message";
        private const string ModelStateDictionary = "model state dictionary";

        public BadRequestTestBuilder(
            ApiController controller,
            string actionName,
            TBadRequestResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        public void WithErrorMessage()
        {
            var badRequestErrorMessageResult = this.GetBadRequestResult<BadRequestErrorMessageResult>(ErrorMessage);
            // TODO: return error message test builder
        }

        public void WithErrorMessage(string message)
        {
            var badRequestErrorMessageResult = this.GetBadRequestResult<BadRequestErrorMessageResult>(ErrorMessage);
            // TODO: validate message
        }

        public void WithModelState(ModelStateDictionary modelState)
        {
            var invalidModelStateResult = this.GetBadRequestResult<InvalidModelStateResult>(ModelStateDictionary);

            // TODO: validate provided model state dictionary
        }

        public void WithModelStateFor<TRequestModel>()
        {
            var invalidModelStateResult = this.GetBadRequestResult<InvalidModelStateResult>(ModelStateDictionary);

            // TODO: return model state error builder
        }

        private TExpectedBadRequestResult GetBadRequestResult<TExpectedBadRequestResult>(string containment)
            where TExpectedBadRequestResult : class 
        {
            var actualBadRequestResult = this.ActionResult as TExpectedBadRequestResult;
            if (actualBadRequestResult == null)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request result to contains {2}, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyTypeName(),
                    containment));
            }

            return actualBadRequestResult;
        }
    }
}
