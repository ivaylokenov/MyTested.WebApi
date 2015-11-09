// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException ContainingNoModelStateErrors();
    }
}
