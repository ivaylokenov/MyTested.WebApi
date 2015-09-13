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

namespace MyWebApi.Builders.HttpActionResults.Unauthorized
{
    using System;
    using System.Collections.Generic;
    using Contracts.HttpActionResults.Unauthorized;

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
        /// AndAlso method for better readability when chaining header builders.
        /// </summary>
        /// <returns>The same challenge builder.</returns>
        public IChallengesBuilder AndAlso()
        {
            return this;
        }

        internal ICollection<Action<IAuthenticationHeaderValueBuilder>> GetAuthenticationHeaderValueBuilders()
        {
            return this.authenticationHeaderValueBuilders;
        }
    }
}
