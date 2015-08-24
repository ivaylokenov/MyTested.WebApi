namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;

    using Contracts;
    using Exceptions;
    using Utilities;

    public class ResponseModelDetailsTestBuilder<TResponseModel> : ResponseModelErrorTestBuilder<TResponseModel>, IResponseModelDetailsTestBuilder<TResponseModel>
    {
        private readonly TResponseModel actualModel;

        public ResponseModelDetailsTestBuilder(ApiController controller, string actionName, TResponseModel actualModel)
            : base(controller, actionName)
        {
            this.actualModel = actualModel;
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions)
        {
            assertions(actualModel);
            return new ResponseModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(actualModel))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When calling {0} action in {1} expected response model {2} to pass the given condition, but it failed.",
                            this.ActionName,
                            this.Controller.GetType().ToFriendlyTypeName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return new ResponseModelErrorTestBuilder<TResponseModel>(this.Controller, this.ActionName);
        }
    }
}
