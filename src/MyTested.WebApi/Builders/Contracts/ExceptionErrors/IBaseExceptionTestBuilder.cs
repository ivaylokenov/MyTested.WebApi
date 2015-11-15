// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing expected exception messages.
    /// </summary>
    public interface IBaseExceptionTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests exception message using test builder.
        /// </summary>
        /// <returns>Exception message test builder.</returns>
        IExceptionMessageTestBuilder WithMessage();

        /// <summary>
        /// Tests exception message whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected exception message as string.</param>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder WithMessage(string message);
    }
}
