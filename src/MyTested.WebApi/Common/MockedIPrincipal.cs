// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Mocked IPrinciple object.
    /// </summary>
    public class MockedIPrincipal : IPrincipal
    {
        private const string DefaultIdentifier = "TestId";
        private const string DefaultUsername = "TestUser";
        private const string DefaultAuthenticationType = "Passport";

        private readonly IEnumerable<string> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedIPrincipal" /> class.
        /// </summary>
        /// <param name="authenticationType">Initial principal type.</param>
        /// <param name="claims">Initial user claims.</param>
        /// <param name="roles">Initial user roles.</param>
        public MockedIPrincipal(
            string authenticationType = null,
            ICollection<Claim> claims = null,
            ICollection<string> roles = null)
        {
            this.roles = roles ?? new HashSet<string>();
            this.Identity = GetAuthenticatedClaimsIdentity(claims, authenticationType);
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
            return new MockedIPrincipal
            {
                Identity = new ClaimsIdentity()
            };
        }

        /// <summary>
        /// Static constructor for creating default authenticated mocked user object with "TestUser" username.
        /// </summary>
        /// <returns>Authenticated IPrincipal.</returns>
        public static IPrincipal CreateDefaultAuthenticated()
        {
            return new MockedIPrincipal
            {
                Identity = GetAuthenticatedClaimsIdentity()
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

        private static ClaimsIdentity GetAuthenticatedClaimsIdentity(
            ICollection<Claim> claims = null,
            string authenticationType = DefaultAuthenticationType)
        {
            claims = claims ?? new List<Claim>();

            if (claims.All(c => c.Type != ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, DefaultIdentifier));
            }

            if (claims.All(c => c.Type != ClaimTypes.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, DefaultUsername));
            }

            return new ClaimsIdentity(claims, authenticationType);
        }
    }
}
