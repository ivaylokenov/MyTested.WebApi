namespace MyWebApi.Builders.BadRequests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;

    using Base;
    using Common.Extensions;
    using Contracts;
    using Exceptions;

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
            var actualMessage = badRequestErrorMessageResult.Message;
            this.ValidateErrorMessage(message, actualMessage);
        }

        public void WithModelState(ModelStateDictionary modelState)
        {
            var invalidModelStateResult = this.GetBadRequestResult<InvalidModelStateResult>(ModelStateDictionary);
            var actualModelState = invalidModelStateResult.ModelState;

            var actualModelStateSortedKeys = actualModelState.Keys.OrderBy(k => k);
            var expectedModelStateSortedKeys = modelState.Keys.OrderBy(k => k);

            foreach (var expectedKey in expectedModelStateSortedKeys)
            {
                if (!actualModelState.ContainsKey(expectedKey))
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} key, but none found.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedKey));
                }

                var actualSortedErrors = GetSortedErrorMessagesForModelStateKey(actualModelState[expectedKey].Errors);
                var expectedSortedErrors = GetSortedErrorMessagesForModelStateKey(modelState[expectedKey].Errors);

                if (expectedSortedErrors.Count != actualSortedErrors.Count)
                {
                    throw new BadRequestResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected bad request model state dictionary to contain {2} errors for {3} key, but found {4}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedSortedErrors.Count,
                        expectedKey,
                        actualSortedErrors.Count));
                }

                for (int i = 0; i < expectedSortedErrors.Count; i++)
                {
                    var expectedError = expectedSortedErrors[i];
                    var actualError = actualSortedErrors[i];
                    this.ValidateErrorMessage(expectedError, actualError);
                }
            }
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
                    "When calling {0} action in {1} expected bad request result to contain {2}, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    containment));
            }

            return actualBadRequestResult;
        }

        private void ValidateErrorMessage(string expectedMessage, string actualMessage)
        {
            if (expectedMessage != actualMessage)
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected bad request with message '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedMessage,
                    actualMessage));
            }
        }

        private static IList<string> GetSortedErrorMessagesForModelStateKey(IEnumerable<ModelError> errors)
        {
            return errors
                .OrderBy(er => er.ErrorMessage)
                .Select(er => er.ErrorMessage)
                .ToList();
        }
    }
}
