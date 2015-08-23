namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Base;
    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IResponseModelTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ResponseModelTestBuilder(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        public void WithNoResponseModel()
        {
            var actualResult = this.ActionResult as OkResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                        "When calling {0} action in {1} expected to not have response model but in fact response model was found.",
                        this.ActionName,
                        this.Controller.GetType().ToFriendlyGenericTypeName()));
            }
        }

        /// <summary>
        /// Tests whether certain type of response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>()
        {
            var actionResultType = this.ActionResult.GetType();
            var negotiatedContentResultType = typeof(OkNegotiatedContentResult<TResponseModel>);

            var negotiatedActionResultIsAssignable = Reflection.AreAssignable(
                actionResultType,
                negotiatedContentResultType);
            if (!negotiatedActionResultIsAssignable)
            {
                if (actionResultType.IsGenericType)
                {
                    var actualResponseDataType = actionResultType.GetGenericArguments()[0];
                    var expectedResponseDataType = typeof(TResponseModel);

                    var responseDataTypeIsAssignable = Reflection.AreAssignable(
                        actualResponseDataType,
                        expectedResponseDataType);
                    if (!responseDataTypeIsAssignable)
                    {
                        throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model to be a {2}, but instead received a {3}.",
                            this.ActionName,
                            this.Controller.GetType().ToFriendlyGenericTypeName(),
                            typeof(TResponseModel).ToFriendlyGenericTypeName(),
                            actualResponseDataType.ToFriendlyGenericTypeName()));
                    }
                }
            }

            return new ResponseModelErrorTestBuilder<TResponseModel>(Controller, ActionName);
        }

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class
        {
            this.WithResponseModel<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            if (actualModel != expectedModel)
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to be the given model, but in fact it was a different model.",
                            this.ActionName,
                            this.Controller.GetType().ToFriendlyGenericTypeName(),
                            typeof(TResponseModel).ToFriendlyGenericTypeName()));
            }

            return new ResponseModelErrorTestBuilder<TResponseModel>(Controller, ActionName);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(Action<TResponseModel> assertions)
        {
            this.WithResponseModel<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            assertions(actualModel);

            return new ResponseModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(Func<TResponseModel, bool> predicate)
        {
            this.WithResponseModel<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            if (!predicate(actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                            this.ActionName,
                            this.Controller.GetType().ToFriendlyGenericTypeName(),
                            typeof(TResponseModel).ToFriendlyGenericTypeName()));
            }

            return new ResponseModelErrorTestBuilder<TResponseModel>(Controller, ActionName);
        }

        private TResponseModel GetActualModel<TResponseModel>()
        {
            return this.ActionResult.GetType().CastTo<dynamic>(this.ActionResult).Content;
        }
    }
}
