// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using Common;
    using Common.Extensions;
    using Contracts;

    /// <summary>
    /// Used for building mocked Controller.User object.
    /// </summary>
    public class UserBuilder : IUserBuilder
    {
        private readonly ICollection<Claim> constructedClaims; 
        private readonly ICollection<string> constructedRoles;

        private string constructedAuthenticationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBuilder" /> class.
        /// </summary>
        public UserBuilder()
        {
            this.constructedClaims = new List<Claim>();
            this.constructedRoles = new HashSet<string>();
        }

        public IUserBuilder WithIdentifier(string identifier)
        {
            this.constructedClaims.Add(new Claim(ClaimTypes.NameIdentifier, identifier));
            return this;
        }

        /// <summary>
        /// Used for setting username to the mocked user object.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder WithUsername(string username)
        {
            this.constructedClaims.Add(new Claim(ClaimTypes.Name, username));
            return this;
        }

        public IUserBuilder WithClaim(Claim claim)
        {
            this.constructedClaims.Add(claim);
            return this;
        }

        /// <summary>
        /// Used for setting authentication type to the mocked user object.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder WithAuthenticationType(string authenticationType)
        {
            this.constructedAuthenticationType = authenticationType;
            return this;
        }

        /// <summary>
        /// Used for adding user role to the mocked user object.
        /// </summary>
        /// <param name="role">The user role to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRole(string role)
        {
            this.constructedRoles.Add(role);
            return this;
        }

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRoles(IEnumerable<string> roles)
        {
            roles.ForEach(role => this.constructedRoles.Add(role));
            return this;
        }

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same user builder.</returns>
        public IUserBuilder InRoles(params string[] roles)
        {
            return this.InRoles(roles.AsEnumerable());
        }

        internal IPrincipal GetUser()
        {
            return new MockedIPrincipal( 
                this.constructedAuthenticationType, 
                this.constructedClaims,
                this.constructedRoles);
        }
    }
}
