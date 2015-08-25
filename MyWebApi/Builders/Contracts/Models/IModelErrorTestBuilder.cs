namespace MyWebApi.Builders.Contracts.Models
{
    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    public interface IModelErrorTestBuilder
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        void ContainingNoModelStateErrors();
    }
}
