namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    using Common;

    /// <summary>
    /// Used for testing the authenticated header challenges in unauthorized results.
    /// </summary>
    public interface IUnauthorizedResultTestBuilder
    {
        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided default scheme.
        /// </summary>
        /// <param name="scheme">Enumeration containing default schemes.</param>
        /// <returns>Unauthorized result test builder with And() method.</returns>
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationScheme scheme);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided scheme as string.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <returns>Unauthorized result test builder with And() method.</returns>
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided scheme and parameter.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <param name="parameter">Parameter as string.</param>
        /// <returns>Unauthorized result test builder with And() method.</returns>
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme, string parameter);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided authenticated header value.
        /// </summary>
        /// <param name="challenge">AuthenticationHeaderValue containing scheme and parameter.</param>
        /// <returns>Unauthorized result test builder with And() method.</returns>
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header using the provided authenticated header value builder.
        /// </summary>
        /// <param name="challengeBuilder">Builder for creating AuthenticationHeaderValue.</param>
        /// <returns>Unauthorized result test builder with And() method.</returns>
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(
            Action<IAuthenticationHeaderValueBuilder> challengeBuilder);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authenticated header values as the provided collection.
        /// </summary>
        /// <param name="challenges">Collection of authenticated header values.</param>
        void WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authenticated header values as the provided ones as parameters.
        /// </summary>
        /// <param name="challenges">Parameters of authenticated header values.</param>
        void WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authentication header values as the provided ones from the challenges builder.
        /// </summary>
        /// <param name="challengesBuilder">Builder for creating collection of authentication header values.</param>
        void WithAuthenticationHeaderChallenges(Action<IChallengesBuilder> challengesBuilder);
    }
}
