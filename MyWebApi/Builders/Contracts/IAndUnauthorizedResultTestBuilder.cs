namespace MyWebApi.Builders.Contracts
{
    /// <summary>
    /// Used for adding And() method to the the unauthorized response tests.
    /// </summary>
    public interface IAndUnauthorizedResultTestBuilder : IUnauthorizedResultTestBuilder
    {
        /// <summary>
        /// And method for better readability when chaining unauthorized result tests.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedResultTestBuilder And();
    }
}
