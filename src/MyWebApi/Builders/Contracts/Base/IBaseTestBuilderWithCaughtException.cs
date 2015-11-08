// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Base
{
    using System;

    /// <summary>
    /// Base interface for test builders with caught exception.
    /// </summary>
    public interface IBaseTestBuilderWithCaughtException : IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        Exception AndProvideTheCaughtException();
    }
}
