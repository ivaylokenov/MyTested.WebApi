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

namespace MyWebApi.Common.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    /// <summary>
    /// Mocked IPrinciple object.
    /// </summary>
    public class MockedIPrinciple : IPrincipal
    {
        private const string DefaultUsername = "TestUser";
        private const string DefaultIPrincipalType = "Passport";

        private readonly IEnumerable<string> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedIPrinciple" /> class.
        /// </summary>
        /// <param name="username">Initial username.</param>
        /// <param name="principalType">Initial principal type.</param>
        /// <param name="roles">Initial user roles.</param>
        public MockedIPrinciple(
            string username = null,
            string principalType = null,
            IEnumerable<string> roles = null)
        {
            this.roles = roles ?? new HashSet<string>();
            this.Identity = new MockedIIdentity(
                username ?? DefaultUsername,
                principalType ?? DefaultIPrincipalType,
                true);
        }

        /// <summary>
        /// Gets the IIdentity of the IPrinciple.
        /// </summary>
        /// <value>IIdentity object.</value>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Static constructor for creating default unauthenticated mocked user object.
        /// </summary>
        /// <returns>Unauthenticated IPrincipal.</returns>
        public static IPrincipal CreateUnauthenticated()
        {
            return new MockedIPrinciple
            {
                Identity = new MockedIIdentity()
            };
        }

        /// <summary>
        /// Static constructor for creating default authenticated mocked user object with "TestUser" username.
        /// </summary>
        /// <returns>Authenticated IPrincipal.</returns>
        public static IPrincipal CreateDefaultAuthenticated()
        {
            return new MockedIPrinciple()
            {
                Identity = new MockedIIdentity(DefaultUsername, DefaultIPrincipalType, true)
            };
        }

        /// <summary>
        /// Checks whether the current user is in user role.
        /// </summary>
        /// <param name="role">User role to check.</param>
        /// <returns>True or False.</returns>
        public bool IsInRole(string role)
        {
            return this.roles.Contains(role);
        }
    }
}
