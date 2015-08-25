namespace MyWebApi.Builders.UnauthorizedResults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Base;
    using Contracts;

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
                // TODO: error
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

            for (int i = 0; i < actualChallenges.Count; i++)
            {
                var actualChallenge = actualChallenges[i];
                var expectedChallenge = expectedChallenges[i];
                if (actualChallenge.Parameter != expectedChallenge.Parameter
                    || actualChallenge.Scheme != expectedChallenge.Scheme)
                {
                    // todo: ERROR
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
    }
}
