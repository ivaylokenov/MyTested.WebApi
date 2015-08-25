namespace MyWebApi.Builders.Contracts.Models
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
    }
}
