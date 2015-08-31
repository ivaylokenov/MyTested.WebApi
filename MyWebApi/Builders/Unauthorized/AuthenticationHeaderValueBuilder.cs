namespace MyWebApi.Builders.Unauthorized
{
    using System.Net.Http.Headers;
    using Common;
    using Contracts.Unauthorized;

    /// <summary>
    /// Used for building mocked AuthenticationHeaderValue.
    /// </summary>
    public class AuthenticationHeaderValueBuilder : IAuthenticationHeaderValueBuilder, IAuthenticationHeaderValueParameterBuilder
    {
        private readonly AuthenticationHeaderValueMock authenticatedHeaderValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationHeaderValueBuilder" /> class.
        /// </summary>
        public AuthenticationHeaderValueBuilder()
        {
            this.authenticatedHeaderValue = new AuthenticationHeaderValueMock();
        }

        /// <summary>
        /// Sets scheme to the built authentication header value with the provided AuthenticationScheme enumeration.
        /// </summary>
        /// <param name="scheme">Enumeration with default authentication header schemes.</param>
        /// <returns>Authentication header value parameter builder.</returns>
        public IAuthenticationHeaderValueParameterBuilder WithScheme(AuthenticationScheme scheme)
        {
            this.authenticatedHeaderValue.Scheme = scheme.ToString();
            return this;
        }

        /// <summary>
        /// Sets scheme to the built authentication header value with the provided string.
        /// </summary>
        /// <param name="scheme">Authentication header value scheme as string.</param>
        /// <returns>Authentication header value parameter builder.</returns>
        public IAuthenticationHeaderValueParameterBuilder WithScheme(string scheme)
        {
            this.authenticatedHeaderValue.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Sets parameter to the built authentication header value with the provided string.
        /// </summary>
        /// <param name="parameter">Authentication header value parameter as string.</param>
        public void WithParameter(string parameter)
        {
            this.authenticatedHeaderValue.Parameter = parameter;
        }

        internal AuthenticationHeaderValue GetAuthenticationHeaderValue()
        {
            return new AuthenticationHeaderValue(this.authenticatedHeaderValue.Scheme, this.authenticatedHeaderValue.Parameter);
        }

        private class AuthenticationHeaderValueMock
        {
            public string Scheme { get; set; }

            public string Parameter { get; set; }
        }
    }
}
