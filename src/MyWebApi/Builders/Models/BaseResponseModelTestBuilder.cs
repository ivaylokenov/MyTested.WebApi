namespace MyWebApi.Builders.Models
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Base;
    using Common.Extensions;
    using Contracts.Models;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Base class for all response model test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class BaseResponseModelTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponseModelTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public BaseResponseModelTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether certain type of response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
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
                            expectedResponseDataType,
                            actualResponseDataType);

                    if (!responseDataTypeIsAssignable)
                    {
                        throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model to be a {2}, but instead received a {3}.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName(),
                            actualResponseDataType.ToFriendlyTypeName()));
                    }
                }
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.GetActualModel<TResponseModel>());
        }

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class
        {
            this.WithResponseModelOfType<TResponseModel>();

            var actualModel = this.GetActualModel<TResponseModel>();
            if (actualModel != expectedModel)
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to be the given model, but in fact it was a different model.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }
    }
}
