// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.ExceptionErrors
{
    using System;
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
        [Obsolete("This method will be removed from version 1.4 and above. Please, use the Action and Func overloads instead.")]
        IExceptionMessageTestBuilder WithMessage();

        /// <summary>
        /// Tests exception message whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected exception message as string.</param>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder WithMessage(string message);

        /// <summary>
        /// Tests exception message whether it passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on message.</param>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder WithMessage(Action<string> assertions);

        /// <summary>
        /// Tests exception message whether it passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the message.</param>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder WithMessage(Func<string, bool> predicate);
    }
}
