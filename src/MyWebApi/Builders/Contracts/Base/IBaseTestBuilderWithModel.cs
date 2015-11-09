// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Base
{
    /// <summary>
    /// Base interface for all test builders with model.
    /// </summary>
    /// <typeparam name="TModel">Model returned from action result.</typeparam>
    public interface IBaseTestBuilderWithModel<out TModel> : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Gets the model returned from an action result.
        /// </summary>
        /// <returns>Model returned from action result.</returns>
        TModel AndProvideTheModel();
    }
}
