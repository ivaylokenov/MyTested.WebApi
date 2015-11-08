// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Common
{
    /// <summary>
    /// Represents void action result in generic test builder.
    /// </summary>
    public class VoidActionResult
    {
        /// <summary>
        /// Creates new instance of <see cref="VoidActionResult"/>.
        /// </summary>
        /// <returns>Void action result.</returns>
        public static VoidActionResult Create()
        {
            return new VoidActionResult();
        }
    }
}
