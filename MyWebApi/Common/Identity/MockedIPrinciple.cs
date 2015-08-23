namespace MyWebApi.Common.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    public class MockedIPrinciple : IPrincipal
    {
        private const string DefaultUsername = "TestUser";
        private const string DefaultIPrincipalType = "Passport";

        private readonly IEnumerable<string> roles;

        public MockedIPrinciple()
        {
            this.roles = new HashSet<string>();
        }

        public static IPrincipal CreateUnauthorized()
        {
            return new MockedIPrinciple
            {
                Identity = new MockedIIdentity()
            };
        }

        public static IPrincipal CreateDefaultAuthorized()
        {
            return new MockedIPrinciple()
            {
                Identity = new MockedIIdentity(DefaultUsername, DefaultIPrincipalType, true)
            };
        }

        public bool IsInRole(string role)
        {
            return this.roles.Contains(role);
        }

        public IIdentity Identity { get; private set; }
    }
}
