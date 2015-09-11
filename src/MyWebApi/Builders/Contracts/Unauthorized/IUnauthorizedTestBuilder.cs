// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Builders.Contracts.Unauthorized
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Web.Http.Results;
    using Base;

    /// <summary>
    /// Used for testing the authenticated header challenges in unauthorized results.
    /// </summary>
    public interface IUnauthorizedTestBuilder : IBaseTestBuilderWithActionResult<UnauthorizedResult>
    {
        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided default scheme.
        /// </summary>
        /// <param name="scheme">Enumeration containing default schemes.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationScheme scheme);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided scheme as string.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(string scheme);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided scheme and parameter.
        /// </summary>
        /// <param name="scheme">Scheme as string.</param>
        /// <param name="parameter">Parameter as string.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(string scheme, string parameter);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header with the provided authenticated header value.
        /// </summary>
        /// <param name="challenge">AuthenticationHeaderValue containing scheme and parameter.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(AuthenticationHeaderValue challenge);

        /// <summary>
        /// Tests whether an unauthorized result contains authenticated header using the provided authenticated header value builder.
        /// </summary>
        /// <param name="challengeBuilder">Builder for creating AuthenticationHeaderValue.</param>
        /// <returns>Unauthorized result test builder with AndAlso() method.</returns>
        IAndUnauthorizedTestBuilder ContainingAuthenticationHeaderChallenge(
            Action<IAuthenticationHeaderValueBuilder> challengeBuilder);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authenticated header values as the provided collection.
        /// </summary>
        /// <param name="challenges">Collection of authenticated header values.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(IEnumerable<AuthenticationHeaderValue> challenges);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authenticated header values as the provided ones as parameters.
        /// </summary>
        /// <param name="challenges">Parameters of authenticated header values.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(params AuthenticationHeaderValue[] challenges);

        /// <summary>
        /// Tests whether an unauthorized result has exactly the same authentication header values as the provided ones from the challenges builder.
        /// </summary>
        /// <param name="challengesBuilder">Builder for creating collection of authentication header values.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<UnauthorizedResult> WithAuthenticationHeaderChallenges(Action<IChallengesBuilder> challengesBuilder);
    }
}
