namespace MyWebApi.Builders.Contracts.Unauthorized
{
    /// <summary>
    /// Used for building mocked AuthenticationHeaderValue parameter.
    /// </summary>
    public interface IAuthenticationHeaderValueParameterBuilder
    {
        /// <summary>
        /// Sets parameter to the built authentication header value with the provided string.
        /// </summary>
        /// <param name="parameter">Authentication header value parameter as string.</param>
        void WithParameter(string parameter);
    }
}
