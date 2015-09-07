namespace MyWebApi.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing the response model type of action.
    /// </summary>
    public interface IResponseModelTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder WithNoResponseModel();
    }
}
