// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the the model error tests.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IAndModelErrorTestBuilder<TModel> : IModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> AndAlso();
    }
}
