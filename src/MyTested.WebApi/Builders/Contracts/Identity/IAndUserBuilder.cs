// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Identity
{
    /// <summary>
    /// Used for adding AndAlso() method to the the built user.
    /// </summary>
    public interface IAndUserBuilder : IUserBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building user.
        /// </summary>
        /// <returns>The same user builder.</returns>
        IUserBuilder AndAlso();
    }
}
