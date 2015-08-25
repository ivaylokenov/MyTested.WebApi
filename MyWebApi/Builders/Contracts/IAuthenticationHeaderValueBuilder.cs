namespace MyWebApi.Builders.Contracts
{
    using Common;

    /// <summary>
    /// Used for building mocked AuthenticationHeaderValue scheme.
    /// </summary>
    public interface IAuthenticationHeaderValueBuilder
    {
        /// <summary>
        /// Sets scheme to the built authentication header value with the provided AuthenticationScheme enumeration.
        /// </summary>
        /// <param name="scheme">Enumeration with default authentication header schemes.</param>
        /// <returns>Authentication header value parameter builder.</returns>
        IAuthenticationHeaderValueParameterBuilder WithScheme(AuthenticationScheme scheme);

        /// <summary>
        /// Sets scheme to the built authentication header value with the provided string.
        /// </summary>
        /// <param name="scheme">Authentication header scheme as string.</param>
        /// <returns>Authentication header value parameter builder.</returns>
        IAuthenticationHeaderValueParameterBuilder WithScheme(string scheme);
    }
}
