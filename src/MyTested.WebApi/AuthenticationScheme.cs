// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi
{
    /// <summary>
    /// Contains default authentication header schemes.
    /// </summary>
    public enum AuthenticationScheme
    {
        /// <summary>
        /// Anonymous authentication header scheme.
        /// </summary>
        Anonymous,

        /// <summary>
        /// Basic authentication header scheme.
        /// </summary>
        Basic,

        /// <summary>
        /// Digest authentication header scheme.
        /// </summary>
        Digest,

        /// <summary>
        /// NTLM authentication header scheme.
        /// </summary>
        NTLM,

        /// <summary>
        /// Negotiate authentication header scheme.
        /// </summary>
        Negotiate
    }
}
