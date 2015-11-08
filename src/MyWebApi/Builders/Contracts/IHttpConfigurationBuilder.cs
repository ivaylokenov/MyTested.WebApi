// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts
{
    /// <summary>
    /// HTTP configuration builder.
    /// </summary>
    public interface IHttpConfigurationBuilder
    {
        /// <summary>
        /// Starts HTTP server with the provided configuration.
        /// </summary>
        void AndStartsServer();
    }
}
