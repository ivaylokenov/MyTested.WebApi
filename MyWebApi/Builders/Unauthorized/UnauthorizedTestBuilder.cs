namespace MyWebApi.Builders.Unauthorized
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Base;
    using Common;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.UnauthorizedResults;
    using Exceptions;

    /// <summary>
    /// Used for testing the authentication header challenges in unauthorized results.
    /// </summary>
    public class UnauthorizedTestBuilder : BaseTestBuilderWithActionResult<UnauthorizedResult>,
        IUnauthorizedTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public UnauthorizedTestBuilder(
            ApiController controller,
            string actionName,
            UnauthorizedResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        /// <summary>
        /// Tests whether an unauthorized result contains authentication header with the provided default scheme.
        /// </summary>
        /// <param name="scheme">Enumeration containing default schemes.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        public IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationScheme scheme)
        {
            return this.ContainingAuthenticationHeaderChallenge(scheme.ToString());
        }

        /// <summary>
        /// Tests whether an unauthorized result contains authentication header with the provided scheme as string.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        public IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(string scheme)
        {
            if (this.ActionResult.Challenges.All(c => c.Scheme != scheme))
            {
                this.ThrowNewUnathorizedResultAssertionException(scheme);
            }

            return new AndUnauthorizedTestBuilder(this.Controller, this.ActionName, this.ActionResult, this);
        }

        /// <summary>
        /// Tests whether an unauthorized result contains authentication header with the provided scheme and parameter.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <param name="parameter">Parameter as string.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        public IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(string scheme, string parameter)
        {
            if (!this.ActionResult.Challenges.Any(c => c.Scheme == scheme
                && c.Parameter == parameter))
            {
                this.ThrowNewUnathorizedResultAssertionException(scheme, parameter);
            }

            return new AndUnauthorizedTestBuilder(this.Controller, this.ActionName, this.ActionResult, this);
        }

        /// <summary>
        /// Tests whether an unauthorized result contains authentication header with the provided authenticated header value.
        /// </summary>
        /// <param name="challenge">AuthenticationHeaderValue containing scheme and parameter.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        public IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge)
        {
            if (string.IsNullOrEmpty(challenge.Parameter))
            {
                return this.ContainingAuthenticationHeaderChallenge(challenge.Scheme);
            }

            return this.ContainingAuthenticationHeaderChallenge(challenge.Scheme, challenge.Parameter);
        }

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header using the provided authenticated header value builder.
        /// </summary>
        /// <param name="challengeBuilder">Builder for creating AuthenticationHeaderValue.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        public IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(
            Action<IAuthenticationHeaderValueBuilder> challengeBuilder)
        {
            var authenticationHeaderValue = this.GetAuthenticationHeaderValue(challengeBuilder);
            return this.ContainingAuthenticationHeaderChallenge(authenticationHeaderValue);
        }

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authentication header values as the provided collection.
        /// </summary>
        /// <param name="challenges">Collection of authentication header values.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges)
        {
            var actualChallenges = SortChallenges(this.ActionResult.Challenges);
            var expectedChallenges = SortChallenges(challenges);

            if (actualChallenges.Count != expectedChallenges.Count)
            {
                throw new UnauthorizedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have {2} authentication header challenges, but found {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
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

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authentication header values as the provided ones as parameters.
        /// </summary>
        /// <param name="challenges">Parameters of authentication header values.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges)
        {
            this.WithAuthenticationHeaderChallenges(challenges.AsEnumerable());
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authentication header values as the provided ones from the challenges builder.
        /// </summary>
        /// <param name="challengesBuilder">Builder for creating collection of authentication header values.</param>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(Action<IChallengesBuilder> challengesBuilder)
        {
            var newChallengesBuilder = new ChallengesBuilder();
            challengesBuilder(newChallengesBuilder);
            var authenticationHeaderBuilders = newChallengesBuilder.GetAuthenticationHeaderValueBuilders();

            var authenticationHeaders = authenticationHeaderBuilders
                .Select(this.GetAuthenticationHeaderValue)
                .AsEnumerable();

            this.WithAuthenticationHeaderChallenges(authenticationHeaders);
            return this.NewAndProvideTestBuilder();
        }

        private static IList<AuthenticationHeaderValue> SortChallenges(
            IEnumerable<AuthenticationHeaderValue> challenges)
        {
            return challenges
                .OrderBy(c => c.Parameter)
                .ThenBy(c => c.Scheme)
                .ToList();
        }

        private AuthenticationHeaderValue GetAuthenticationHeaderValue(Action<AuthenticationHeaderValueBuilder> challengeBuilder)
        {
            var authenticationHeaderValueBuilder = new AuthenticationHeaderValueBuilder();
            challengeBuilder(authenticationHeaderValueBuilder);
            return authenticationHeaderValueBuilder.GetAuthenticationHeaderValue();
        }

        private void ThrowNewUnathorizedResultAssertionException(string scheme, string parameter = null)
        {
            throw new UnauthorizedResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have authentication header challenge with {2} scheme and {3} parameter, but none found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    scheme,
                    parameter ?? "no matter what"));
        }
    }
}
