namespace MyWebApi.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        IBaseTestBuilder ContainingNoModelStateErrors();
    }
}
