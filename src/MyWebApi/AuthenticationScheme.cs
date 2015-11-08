// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi
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
