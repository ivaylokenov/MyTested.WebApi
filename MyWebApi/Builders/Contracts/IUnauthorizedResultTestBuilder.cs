namespace MyWebApi.Builders.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    using Common;

    public interface IUnauthorizedResultTestBuilder
    {
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationScheme scheme);

        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme);

        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme, string parameter);

        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge);

        void WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges);

        void WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges);
    }
}
