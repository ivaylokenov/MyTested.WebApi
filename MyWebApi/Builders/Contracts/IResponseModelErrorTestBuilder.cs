namespace MyWebApi.Builders.Contracts
{
    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    public interface IResponseModelErrorTestBuilder
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        void ContainingNoModelStateErrors();

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        void ContainingModelStateError(string errorKey);
    }
}
