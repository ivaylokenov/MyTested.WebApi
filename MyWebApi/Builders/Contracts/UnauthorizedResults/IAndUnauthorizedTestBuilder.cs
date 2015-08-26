namespace MyWebApi.Builders.Contracts.UnauthorizedResults
{
    /// <summary>
    /// Used for adding AndAlso() method to the the unauthorized response tests.
    /// </summary>
    public interface IAndUnauthorizedTestBuilder : IUnauthorizedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining unauthorized result tests.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedTestBuilder AndAlso();
    }
}
