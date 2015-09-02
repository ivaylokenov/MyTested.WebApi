namespace MyWebApi.Builders.Contracts.Actions
{
    using Base;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public interface IVoidActionResultTestBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether action result is void.
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilder ShouldReturnEmpty();
    }
}
