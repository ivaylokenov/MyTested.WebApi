// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Base
{
    using System.Net.Http;

    /// <summary>
    /// Base class for handler test builders.
    /// </summary>
    public interface IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Gets the HTTP message handler used in the testing.
        /// </summary>
        /// <returns>Instance of HttpMessageHandler.</returns>
        HttpMessageHandler AndProvideTheHandler();
    }
}
