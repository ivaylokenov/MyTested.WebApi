namespace MyWebApi.Builders.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public interface IUnauthorizedResultTestBuilder
    {
        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string parameter, string scheme);

        IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge);

        void WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges);

        void WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges);
    }
}
