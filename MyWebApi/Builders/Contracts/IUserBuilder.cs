namespace MyWebApi.Builders.Contracts
{
    using System.Collections.Generic;
    using System.Security.Principal;

    public interface IUserBuilder
    {
        IUserBuilder WithUsername(string username);

        IUserBuilder WithAuthenticationType(string authenticationType);

        IUserBuilder InRole(string role);

        IUserBuilder InRoles(IEnumerable<string> roles);

        IUserBuilder InRoles(params string[] roles);
    }
}
