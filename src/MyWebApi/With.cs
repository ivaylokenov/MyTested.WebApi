// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).

namespace MyTested.WebApi
{
    /// <summary>
    /// Provides easier parameter selection in lambda expression where the value of the parameter does not matter.
    /// </summary>
    public class With
    {
        /// <summary>
        /// Provides any parameter to lambda expression where the value of the parameter does not matter.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>Default value of TParameter.</returns>
        public static TParameter Any<TParameter>()
        {
            return default(TParameter);
        }
    }
}
