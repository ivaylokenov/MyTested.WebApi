namespace MyWebApi.Builders.Contracts.Models
{
    using Base;

    public interface IBaseResponseModelTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class;
    }
}
