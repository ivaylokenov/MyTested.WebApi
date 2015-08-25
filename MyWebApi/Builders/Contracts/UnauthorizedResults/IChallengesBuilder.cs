namespace MyWebApi.Builders.Contracts.UnauthorizedResults
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
        IAndChallengesBuilder ContainingHeader(Action<IAuthenticationHeaderValueBuilder> authenticationHeaderValueBuilder);
    }
}
