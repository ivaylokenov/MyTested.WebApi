namespace MyWebApi.Builders
{
    using System.Web.Http.Results;

    using Contracts;
    using Utilities;

    public class ResponseModelTestBuilder<TActionResult>
        : BaseTestBuilder<TActionResult>, IResponseModelTestBuilder<TActionResult>
    {
        public ResponseModelTestBuilder(string actionName, TActionResult actionResult)
            : base(actionName, actionResult)
        {
        }

        public void WithResponseModel<TResponseData>()
        {
            var actionResultType = this.ActionResult.GetType();
            var negotiatedContentResultType = typeof(OkNegotiatedContentResult<TResponseData>);

            var negotiatedActionResultIsAssignable = ReflectionChecker.AreAssignable(
                actionResultType,
                negotiatedContentResultType);
            if (!negotiatedActionResultIsAssignable)
            {
                if (actionResultType.IsGenericType)
                {
                    var actualResponseDataType = actionResultType.GetGenericArguments()[0];
                    var expectedResponseDataType = typeof(TResponseData);

                    var responseDataTypeIsAssignable = ReflectionChecker.AreAssignable(
                        actualResponseDataType,
                        expectedResponseDataType);
                    if (!responseDataTypeIsAssignable)
                    {
                        throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} expected response model to be a {1}, but instead received a {2}.",
                            this.ActionName,
                            typeof(TResponseData).Name,
                            actualResponseDataType.Name));
                    }
                }
            }
        }
    }
}
