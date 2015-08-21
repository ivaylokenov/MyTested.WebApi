namespace MyWebApi.Builders
{
    using System;
    using System.Web.Http.Results;

    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelTestBuilder<TActionResult>
        : BaseTestBuilder<TActionResult>, IResponseModelTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ResponseModelTestBuilder(string actionName, TActionResult actionResult)
            : base(actionName, actionResult)
        {
        }

        /// <summary>
        /// Tests whether certain type of response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
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

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        public void WithResponseModel<TResponseData>(TResponseData expectedModel)
            where TResponseData : class
        {
            this.WithResponseModel<TResponseData>();

            var actualModel = this.GetActualModel<TResponseData>();
            if (actualModel != expectedModel)
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} expected response model to be the given model, but in fact it was a different model.",
                            this.ActionName));
            }
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        public void WithResponseModel<TResponseData>(Action<TResponseData> assertions)
        {
            this.WithResponseModel<TResponseData>();

            var actualModel = this.GetActualModel<TResponseData>();
            assertions(actualModel);
        }

        private TResponseData GetActualModel<TResponseData>()
        {
            return (this.ActionResult as OkNegotiatedContentResult<TResponseData>).Content;
        }
    }
}
