namespace MyWebApi.Common
{
    /// <summary>
    /// Contains default authenticated header schemes.
    /// </summary>
    public enum AuthenticationScheme
    {
        /// <summary>
        /// Anonymous authenticated header scheme.
        /// </summary>
        Anonymous,

        /// <summary>
        /// Basic authenticated header scheme.
        /// </summary>
        Basic,

        /// <summary>
        /// Digest authenticated header scheme.
        /// </summary>
        Digest,

        /// <summary>
        /// NTLM authenticated header scheme.
        /// </summary>
        NTLM,

        /// <summary>
        /// Negotiate authenticated header scheme.
        /// </summary>
        Negotiate
    }
}
