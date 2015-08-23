namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    using Base;
    using Contracts;
    using Exceptions;

    public class ResponseModelErrorDetailsTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorDetailsTestBuilder<TResponseModel>
    {
        private readonly IResponseModelErrorTestBuilder<TResponseModel> responseModelErrorTestBuilder;
        private readonly string currentErrorKey;
        private readonly IEnumerable<string> aggregatedErrors;

        public ResponseModelErrorDetailsTestBuilder(
            ApiController controller,
            string actionName,
            IResponseModelErrorTestBuilder<TResponseModel> responseModelErrorTestBuilder,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(controller, actionName)
        {
            this.responseModelErrorTestBuilder = responseModelErrorTestBuilder;
            currentErrorKey = errorKey;
            this.aggregatedErrors = aggregatedErrors.Select(me => me.ErrorMessage);
        }

        public IResponseModelErrorTestBuilder<TResponseModel> ThatEquals(string errorMessage)
        {
            if (aggregatedErrors.All(e => e != errorMessage))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key {2} to be '{3}', but instead found '{4}'.",
                    currentErrorKey);
            }

            return responseModelErrorTestBuilder;
        }

        public IResponseModelErrorTestBuilder<TResponseModel> BeginningWith(string beginMessage)
        {
            if (!aggregatedErrors.Any(e => e.StartsWith(beginMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to start with '{3}', but instead found '{4}'.",
                    beginMessage);
            }

            return responseModelErrorTestBuilder;
        }

        public IResponseModelErrorTestBuilder<TResponseModel> EndingWith(string endMessage)
        {
            if (!aggregatedErrors.Any(e => e.EndsWith(endMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to end with '{3}', but instead found '{4}'.",
                    endMessage);
            }

            return responseModelErrorTestBuilder;
        }

        public IResponseModelErrorTestBuilder<TResponseModel> Containing(string containsMessage)
        {
            if (!aggregatedErrors.Any(e => e.Contains(containsMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to contain '{3}', but instead found '{4}'.",
                    containsMessage);
            }

            return responseModelErrorTestBuilder;
        }

        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateErrorFor<TAttribute>(Expression<Func<TResponseModel, TAttribute>> memberWithError)
        {
            return responseModelErrorTestBuilder.ContainingModelStateErrorFor(memberWithError);
        }

        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateError(string errorKey)
        {
            return responseModelErrorTestBuilder.ContainingModelStateError(errorKey);
        }

        public IResponseModelErrorTestBuilder<TResponseModel> ContainingNoModelStateErrorFor<TAttribute>(Expression<Func<TResponseModel, TAttribute>> memberWithNoError)
        {
            return responseModelErrorTestBuilder.ContainingNoModelStateErrorFor(memberWithNoError);
        }

        private void ThrowNewResponseModelErrorAssertionException(string messageFormat, string operation)
        {
            throw new ResponseModelErrorAssertionException(string.Format(
                    messageFormat,
                    ActionName,
                    Controller,
                    currentErrorKey,
                    operation,
                    string.Join(", ", aggregatedErrors)));  
        }
    }
}
