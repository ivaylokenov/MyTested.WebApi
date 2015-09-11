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
    using System.Security.Principal;

    /// <summary>
    /// Mocked IIdentity object.
    /// </summary>
    public class MockedIIdentity : IIdentity
    {
        private readonly string name;
        private readonly string authenticationType;
        private readonly bool isAuthenticated;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedIIdentity" /> class.
        /// </summary>
        /// <param name="name">Initial name.</param>
        /// <param name="authenticationType">Initial authentication type.</param>
        /// <param name="isAuthenticated">Initial value for setting whether the user is authenticated or not.</param>
        public MockedIIdentity(string name = null, string authenticationType = null, bool isAuthenticated = false)
        {
            this.name = name;
            this.authenticationType = authenticationType;
            this.isAuthenticated = isAuthenticated;
        }

        /// <summary>
        /// Gets the name of the mocked IIdentity.
        /// </summary>
        /// <value>Name as string.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the authentication type of the mocked IIdentity.
        /// </summary>
        /// <value>Authentication type as string.</value>
        public string AuthenticationType
        {
            get
            {
                return this.authenticationType;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the IIdentity is authenticated or not.
        /// </summary>
        /// <value>True or False.</value>
        public bool IsAuthenticated
        {
            get
            {
                return this.isAuthenticated;
            }
        }
    }
}
