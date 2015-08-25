namespace MyWebApi.Builders.Contracts.UnauthorizedResults
{
    /// <summary>
    /// Used for building collection of AuthenticationHeaderValue with And() method.
    /// </summary>
    public interface IAndChallengesBuilder : IChallengesBuilder
    {
        /// <summary>
        /// And method for better readability when chaining header builders.
        /// </summary>
        /// <returns>The same challenge builder.</returns>
        IChallengesBuilder And();
    }
}
