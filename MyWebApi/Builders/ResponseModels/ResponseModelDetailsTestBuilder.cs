namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.ResponseModels;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelDetailsTestBuilder<TResponseModel>
        : ResponseModelErrorTestBuilder<TResponseModel>, IResponseModelDetailsTestBuilder<TResponseModel>
    {
        private readonly TResponseModel responseModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="responseModel">Response model from invoked action.</param>
        public ResponseModelDetailsTestBuilder(ApiController controller, string actionName, TResponseModel responseModel)
            : base(controller, actionName)
        {
            this.responseModel = responseModel;
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions)
        {
            assertions(this.responseModel);
            return new ResponseModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.responseModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                            this.ActionName,
                            this.Controller.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ResponseModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }
    }
}
