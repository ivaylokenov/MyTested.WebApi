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


        public IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(string parameter, string scheme)
        {
            if (!this.ActionResult.Challenges.Any(c => c.Parameter != parameter || c.Scheme != scheme))
            {
                this.ThrowNewUnathorizedResultAssertionException(parameter, scheme);
            }

            return new AndUnauthorizedTestBuilder(this.Controller, this.ActionName, this.ActionResult, this);
        }

        public IAndUnauthorizedResultTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge)
        {
            return this.ContainingAuthenticationHeaderChallenge(challenge.Parameter, challenge.Scheme);
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
                if (actualChallenge.Parameter != expectedChallenge.Parameter
                    || actualChallenge.Scheme != expectedChallenge.Scheme)
                {
                    this.ThrowNewUnathorizedResultAssertionException(expectedChallenge.Parameter, expectedChallenge.Scheme);
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

        private void ThrowNewUnathorizedResultAssertionException(string parameter, string scheme)
        {
            throw new UnauthorizedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have authentication header challenge with {2} parameter and {3} scheme, but none found.",
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyTypeName(),
                    parameter,
                    scheme));
        }
    }
}
