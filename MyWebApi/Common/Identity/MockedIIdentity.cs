namespace MyWebApi.Common.Identity
{
    using System.Security.Principal;

    public class MockedIIdentity : IIdentity
    {
        private readonly string name;
        private readonly string authenticationType;
        private readonly bool isAuthenticated;

        public MockedIIdentity(string name = null, string authenticationType = null, bool isAuthenticated = false)
        {
            this.name = name;
            this.authenticationType = authenticationType;
            this.isAuthenticated = isAuthenticated;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return this.authenticationType;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.isAuthenticated;
            }
        }
    }
}
