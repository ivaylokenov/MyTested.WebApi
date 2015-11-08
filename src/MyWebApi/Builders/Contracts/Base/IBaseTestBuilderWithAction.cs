// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.Base
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for all test builders with action call.
    /// </summary>
    public interface IBaseTestBuilderWithAction : IBaseTestBuilder
    {
        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <returns>Action name to be tested.</returns>
        string AndProvideTheActionName();

        /// <summary>
        /// Gets the action attributes on the called action.
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the action.</returns>
        IEnumerable<object> AndProvideTheActionAttributes();
    }
}
