namespace MyWebApi.Builders.Unauthorized
{
    using System;
    using System.Collections.Generic;

    using Contracts.UnauthorizedResults;

    /// <summary>
    /// Used for building collection of AuthenticationHeaderValue.
    /// </summary>
    public class ChallengesBuilder : IAndChallengesBuilder
    {
        private readonly ICollection<Action<IAuthenticationHeaderValueBuilder>> authenticationHeaderValueBuilders;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengesBuilder" /> class.
        /// </summary>
        public ChallengesBuilder()
        {
            this.authenticationHeaderValueBuilders = new List<Action<IAuthenticationHeaderValueBuilder>>();
        }

        /// <summary>
        /// Adds built header to the collection of authentication header values.
        /// </summary>
        /// <param name="authenticationHeaderValueBuilder">Action providing authentication header value builder.</param>
        /// <returns>The same challenge builder.</returns>
        public IAndChallengesBuilder ContainingHeader(Action<IAuthenticationHeaderValueBuilder> authenticationHeaderValueBuilder)
        {
            this.authenticationHeaderValueBuilders.Add(authenticationHeaderValueBuilder);
            return this;
        }

        /// <summary>
        /// And method for better readability when chaining header builders.
        /// </summary>
        /// <returns>The same challenge builder.</returns>
        public IChallengesBuilder And()
        {
            return this;
        }

        internal ICollection<Action<IAuthenticationHeaderValueBuilder>> GetAuthenticationHeaderValueBuilders()
        {
            return this.authenticationHeaderValueBuilders;
        }
    }
}
