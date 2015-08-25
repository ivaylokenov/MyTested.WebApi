namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for building collection of AuthenticationHeaderValue.
    /// </summary>
    public interface IChallengesBuilder
    {
        /// <summary>
        /// Adds built header to the collection of authentication header values.
        /// </summary>
        /// <param name="authenticationHeaderValueBuilder">Action providing authentication header value builder.</param>
        /// <returns>The same challenge builder.</returns>
        IChallengesBuilder ContainingHeader(Action<IAuthenticationHeaderValueBuilder> authenticationHeaderValueBuilder);

        /// <summary>
        /// And method for better readability when chaining header builders.
        /// </summary>
        /// <returns>The same challenge builder.</returns>
        IChallengesBuilder And();
    }
}
