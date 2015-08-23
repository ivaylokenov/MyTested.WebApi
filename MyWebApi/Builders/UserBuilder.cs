namespace MyWebApi.Builders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    using Common.Extensions;
    using Common.Identity;

    using Contracts;

    public class UserBuilder : IUserBuilder
    {
        private readonly ICollection<string> constructedRoles;

        private string constructedUsername;
        private string constructedAuthenticationType;

        public UserBuilder()
        {
            this.constructedRoles = new HashSet<string>();
        }

        public IUserBuilder WithUsername(string username)
        {
            this.constructedUsername = username;
            return this;
        }

        public IUserBuilder WithAuthenticationType(string authenticationType)
        {
            this.constructedAuthenticationType = authenticationType;
            return this;
        }

        public IUserBuilder InRole(string role)
        {
            this.constructedRoles.Add(role);
            return this;
        }

        public IUserBuilder InRoles(IEnumerable<string> roles)
        {
            roles.ForEach(role => this.constructedRoles.Add(role));
            return this;
        }

        public IUserBuilder InRoles(params string[] roles)
        {
            return this.InRoles(roles.AsEnumerable());
        }

        internal IPrincipal GetUser()
        {
            return new MockedIPrinciple(
                this.constructedUsername, 
                this.constructedAuthenticationType, 
                this.constructedRoles);
        }
    }
}
