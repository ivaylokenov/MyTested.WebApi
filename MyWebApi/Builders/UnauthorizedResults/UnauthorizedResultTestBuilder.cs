namespace MyWebApi.Builders.UnauthorizedResults
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Base;
    using Contracts;
    using Exceptions;
    using Utilities;

    public class UnauthorizedResultTestBuilder : BaseTestBuilderWithActionResult<UnauthorizedResult>,
        IUnauthorizedResultTestBuilder
    {
        public UnauthorizedResultTestBuilder(
            ApiController controller,
            string actionName,
            UnauthorizedResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        public IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme)
        {
            if (this.ActionResult.Challenges.All(c => c.Scheme != scheme))
            {
                this.ThrowNewUnathorizedResultAssertionException(scheme);
            }

            return new AndUnauthorizedTestBuilder(this.Controller, this.ActionName, this.ActionResult, this);
        }

        public IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string scheme, string parameter)
        {
            if (!this.ActionResult.Challenges.Any(c => c.Scheme == scheme 
                && c.Parameter == parameter))
            {
                this.ThrowNewUnathorizedResultAssertionException(scheme, parameter);
            }

            return new AndUnauthorizedTestBuilder(this.Controller, this.ActionName, this.ActionResult, this);
        }

        public IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge)
        {
            if (string.IsNullOrEmpty(challenge.Parameter))
            {
                return this.ContainingAuthenticationHeaderChallenge(challenge.Scheme);
            }

            return this.ContainingAuthenticationHeaderChallenge(challenge.Scheme, challenge.Parameter);
        }

        public void WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges)
        {
            var actualChallenges = SortChallenges(this.ActionResult.Challenges);
            var expectedChallenges = SortChallenges(challenges);

            if (actualChallenges.Count != expectedChallenges.Count)
            {
                throw new UnauthorizedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} authentication header challenges, but found {3}.",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyTypeName(),
                    expectedChallenges.Count,
                    actualChallenges.Count));
            }

            for (int i = 0; i < actualChallenges.Count; i++)
            {
                var actualChallenge = actualChallenges[i];
                var expectedChallenge = expectedChallenges[i];
                if (actualChallenge.Scheme != expectedChallenge.Scheme
                    || actualChallenge.Parameter != expectedChallenge.Parameter)
                {
                    this.ThrowNewUnathorizedResultAssertionException(expectedChallenge.Scheme, expectedChallenge.Parameter);
                }
            }
        }

        public void WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges)
        {
            this.WithAuthenticationHeaderChallenges(challenges.AsEnumerable());
        }

        private static IList<AuthenticationHeaderValue> SortChallenges(
            IEnumerable<AuthenticationHeaderValue> challenges)
        {
            return challenges
                .OrderBy(c => c.Parameter)
                .ThenBy(c => c.Scheme)
                .ToList();
        }

        private void ThrowNewUnathorizedResultAssertionException(string scheme, string parameter = null)
        {
            throw new UnauthorizedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have authentication header challenge with {2} scheme and {3} parameter, but none found.",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyTypeName(),
                    scheme,
                    parameter ?? "no matter what"));
        }
    }
}
