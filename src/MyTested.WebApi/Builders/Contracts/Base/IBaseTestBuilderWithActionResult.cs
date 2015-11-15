// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IBaseTestBuilderWithActionResult<out TActionResult> : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Gets the tested action result.
        /// </summary>
        /// <returns>Tested action result.</returns>
        TActionResult AndProvideTheActionResult();
    }
}
