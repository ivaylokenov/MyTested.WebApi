// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Common.Extensions
{
    using Utilities;

    /// <summary>
    /// Provides extension methods to Object class.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets friendly type name of object. Useful for generic types.
        /// </summary>
        /// <param name="obj">Object to get friendly name from.</param>
        /// <returns>Friendly name as string.</returns>
        public static string GetName(this object obj)
        {
            return obj.GetType().ToFriendlyTypeName();
        }
    }
}
